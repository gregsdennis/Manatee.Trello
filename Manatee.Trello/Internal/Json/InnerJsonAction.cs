/***************************************************************************************

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
 
	File Name:		InnerJsonAction.cs
	Namespace:		Manatee.Trello.Internal.Json
	Class Name:		InnerJsonAction
	Purpose:		Internal implementation of IJsonAction.

***************************************************************************************/
using System;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Json
{
	internal class InnerJsonAction : IdentifiableJson, IJsonAction
	{
		public string IdMemberCreator { get; set; }
		public IJsonActionData Data { get; set; }
		public string Type { get; set; }
		public DateTime? Date { get; set; }
	}
}