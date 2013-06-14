﻿/***************************************************************************************

	Copyright 2013 Little Crab Solutions

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		Validate.cs
	Namespace:		Manatee.Trello
	Class Name:		Validate
	Purpose:		Exposes validation methods.

***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Contracts;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal
{
	internal static class Validate
	{
		public static void Writable(ITrelloService service)
		{
			if (service == null) return;
			if (service.UserToken == null)
				throw new ReadOnlyAccessException();
		}
		public static void Entity<T>(T entity, bool allowNulls = false)
			where T : ExpiringObject
		{
			if (entity == null)
			{
				if (allowNulls) return;
				throw new ArgumentNullException("entity");
			}
			if (entity.Id == null)
				throw new EntityNotOnTrelloException<T>(entity);
		}
		public static void Nullable<T>(T? value)
			where T : struct
		{
			if (!value.HasValue)
				throw new ArgumentNullException("value");
		}
		public static void NonEmptyString(string str)
		{
			if (string.IsNullOrWhiteSpace(str))
				throw new ArgumentNullException("str");
		}
		public static void Position(Position pos)
		{
			if (pos == null)
				throw new ArgumentNullException("pos");
			if (!pos.IsValid)
				throw new ArgumentException("Cannot set an invalid position.");
		}
		public static string MinStringLength(string str, int minLength, string parameter)
		{
			if (str == null)
				throw new ArgumentNullException("str");
			str = str.Trim();
			if (str.Length < minLength)
				throw new ArgumentException(string.Format("{0} must be at least {1} characters and cannot begin or end with whitespace.", parameter, minLength));
			return str;
		}
		public static string StringLengthRange(string str, int low, int high, string parameter)
		{
			if (str == null)
				throw new ArgumentNullException("str");
			str = str.Trim();
			if (!str.Length.BetweenInclusive(low, high))
				throw new ArgumentException(string.Format("{0} must be from {1} to {2} characters and cannot begin or end with whitespace.", parameter, low, high));
			return str;
		}
		public static string UserName(ITrelloRest svc, string value)
		{
			var retVal = MinStringLength(value, 3, "Username");
			if (retVal != retVal.ToLower())
				throw new ArgumentException("Username may only contain lowercase characters, underscores, and numbers");
			if (svc != null)
			{
				var endpoint = new Endpoint(new[] {"search", "members"});
				var request = svc.RequestProvider.Create(endpoint.ToString());
				request.AddParameter("query", retVal);
				request.AddParameter("fields", "id");
				if (svc.Get<List<IJsonMember>>(request).Count != 0)
					throw new UsernameInUseException(value);
			}
			return retVal;
		}
		public static string OrgName(ITrelloRest svc, string value)
		{
			var retVal = MinStringLength(value, 3, "Name");
			if (retVal != retVal.ToLower())
				throw new ArgumentException("Name may only contain lowercase characters, underscores, and numbers");
			if (svc != null)
			{
				var endpoint = new Endpoint(new[] {"search", retVal});
				var request = svc.RequestProvider.Create(endpoint.ToString());
				request.AddParameter("fields", "id");
				request.AddParameter("modelTypes", "organizations");
				var response = svc.Get<IJsonSearchResults>(request);
				if (response.OrganizationIds.Count != 0)
					throw new OrgNameInUseException(value);
			}
			return retVal;
		}
		public static void Enumeration<T>(T value)
		{
			var validValues = Enum.GetValues(typeof (T)).Cast<T>();
			if (!validValues.Contains(value))
				throw new ArgumentException(string.Format("Value '{0}' is not valid for type '{1}'", value, typeof (T)));
		}
	}
}