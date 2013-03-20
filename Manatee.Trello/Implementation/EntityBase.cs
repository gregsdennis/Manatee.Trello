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
 
	File Name:		EntityBase.cs
	Namespace:		Manatee.Trello.Implementation
	Class Name:		EntityBase
	Purpose:		Base class for entities which are not exclusively owned by
					another entitity.

***************************************************************************************/
using Manatee.Trello.Rest;

namespace Manatee.Trello.Implementation
{
	public abstract class EntityBase : JsonCompatibleExpiringObject
	{
		public string Id { get; protected set; }
		
		public EntityBase() {}
		internal EntityBase(TrelloService svc, string id)
			: base(svc)
		{
			Id = id;
		}
	}
}
