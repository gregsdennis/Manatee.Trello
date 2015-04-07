﻿/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ListCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyListCollection, ListCollection
	Purpose:		Collection objects for lists.

***************************************************************************************/
using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of lists.
	/// </summary>
	public class ReadOnlyListCollection : ReadOnlyCollection<List>
	{
		private readonly TrelloAuthorization _auth;
		private Dictionary<string, object> _additionalParameters;

		internal TrelloAuthorization Auth { get { return _auth; } }

		/// <summary>
		/// Retrieves a list which matches the supplied key.
		/// </summary>
		/// <param name="key">The key to match.</param>
		/// <returns>The matching list, or null if none found.</returns>
		/// <remarks>
		/// Matches on List.Id and List.Name.  Comparison is case-sensitive.
		/// </remarks>
		public List this[string key] { get { return GetByKey(key); } }

		internal ReadOnlyListCollection(string ownerId, TrelloAuthorization auth)
			: base(ownerId)
		{
			_auth = auth ?? TrelloAuthorization.Default;
		}
		internal ReadOnlyListCollection(ReadOnlyListCollection source, TrelloAuthorization auth)
			: base(source.OwnerId)
		{
			_auth = auth ?? TrelloAuthorization.Default;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Lists, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute<List<IJsonList>>(Auth, endpoint, _additionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jl =>
				{
					var list = jl.GetFromCache<List>(_auth);
					list.Json = jl;
					return list;
				}));
		}

		internal void SetFilter(ListFilter cardStatus)
		{
			if (_additionalParameters == null)
				_additionalParameters = new Dictionary<string, object>();
			_additionalParameters["filter"] = cardStatus.GetDescription();
		}

		private List GetByKey(string key)
		{
			return this.FirstOrDefault(l => key.In(l.Id, l.Name));
		}
	}

	/// <summary>
	/// A collection of lists.
	/// </summary>
	public class ListCollection : ReadOnlyListCollection
	{
		internal ListCollection(string ownerId, TrelloAuthorization auth)
			: base(ownerId, auth) { }

		/// <summary>
		/// Creates a new list.
		/// </summary>
		/// <param name="name">The name of the list to add.</param>
		/// <returns>The <see cref="List"/> generated by Trello.</returns>
		public List Add(string name)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] { error });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonList>();
			json.Name = name;
			json.Board = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Board.Id = OwnerId;

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_AddList);
			var newData = JsonRepository.Execute(Auth, endpoint, json);

			return new List(newData, Auth);
		}
	}
}