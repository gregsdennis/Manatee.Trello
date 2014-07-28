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
 
	File Name:		CheckItemCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyCheckItemCollection, CheckItemCollection
	Purpose:		Collection objects for CheckItems.

***************************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of checklist items.
	/// </summary>
	public class ReadOnlyCheckItemCollection : ReadOnlyCollection<CheckItem>
	{
		internal readonly CheckListContext _context;

		internal ReadOnlyCheckItemCollection(CheckListContext context)
			: base(context.Data.Id)
		{
			_context = context;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			_context.Synchronize();
			if (_context.Data.CheckItems == null) return;
			foreach (var jsonCheckItem in _context.Data.CheckItems)
			{
				var checkItem = Items.SingleOrDefault(ci => ci.Id == jsonCheckItem.Id);
				if (checkItem == null)
					Items.Add(new CheckItem(jsonCheckItem, _context.Data.Id, true));
			}
			foreach (var checkItem in Items.ToList())
			{
				if (_context.Data.CheckItems.All(jci => jci.Id != checkItem.Id))
					Items.Remove(checkItem);
			}
		}
	}

	/// <summary>
	/// A collection of checklist items.
	/// </summary>
	public class CheckItemCollection : ReadOnlyCheckItemCollection
	{
		internal CheckItemCollection(CheckListContext context)
			: base(context) { }

		/// <summary>
		/// Creates a new checklist item.
		/// </summary>
		/// <param name="name">The name of the checklist item to add.</param>
		/// <returns>The <see cref="CheckItem"/> generated by Trello.</returns>
		public CheckItem Add(string name)
		{
			var error = NotNullOrWhiteSpaceRule.Instance.Validate(null, name);
			if (error != null)
				throw new ValidationException<string>(name, new[] { error });

			var json = TrelloConfiguration.JsonFactory.Create<IJsonCheckItem>();
			json.Name = name;

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_AddCheckItem, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = JsonRepository.Execute(TrelloAuthorization.Default, endpoint, json);

			return new CheckItem(newData, OwnerId, true);
		}
	}
}