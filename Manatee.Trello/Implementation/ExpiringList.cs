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
 
	File Name:		ExpiringList.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		ExpiringList<TSource, TContent>
	Purpose:		A collection of entities which automatically updates.

***************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Manatee.Trello.Implementation
{
	internal class ExpiringList<TSource, TContent> : ExpiringObject, IEnumerable<TContent>
		where TContent : ExpiringObject, IEquatable<TContent>, new()
		where TSource : EntityBase
	{
		private readonly List<TContent> _list;
		private readonly TSource _source;

		public ExpiringList(TSource source)
		{
			_source = source;
			_list = new List<TContent>();
		}
		public ExpiringList(TrelloService svc, TSource source)
			: base(svc)
		{
			_source = source;
			_list = new List<TContent>();
		}
		public ExpiringList(TrelloService svc, TSource source, IEnumerable<TContent> items)
			: base(svc)
		{
			_source = source;
			_list = new List<TContent>(items);
		}

		public IEnumerator<TContent> GetEnumerator()
		{
			VerifyNotExpired();
			return _list.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		public override string ToString()
		{
			return _list.ToString();
		}

		internal override void Refresh(ExpiringObject entity)
		{
			Refresh();
		}
		internal override bool Match(string id)
		{
			return false;
		}

		protected override sealed void Refresh()
		{
			_list.Clear();
			var entities = Svc.RetrieveContent<TSource, TContent>(_source.Id);
			if (entities == null) return;
			foreach (var entity in entities)
			{
				entity.Svc = Svc;
			}
			_list.AddRange(entities);
		}
		protected override void PropigateSerivce()
		{
			_list.ForEach(i => i.Svc = Svc);
		}
	}
}
