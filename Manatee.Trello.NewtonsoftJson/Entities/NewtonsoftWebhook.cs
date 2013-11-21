﻿/***************************************************************************************

	Copyright 2013 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		NewtonsoftWebhook.cs
	Namespace:		Manatee.Trello.NewtonsoftJson.Entities
	Class Name:		NewtonsoftWebhook
	Purpose:		Implements IJsonWebhook for Newtsoft's Json.Net.

***************************************************************************************/

using Manatee.Trello.Json;
using Newtonsoft.Json;

namespace Manatee.Trello.NewtonsoftJson.Entities
{
	internal class NewtonsoftWebhook : IJsonWebhook
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("idModel")]
		public string IdModel { get; set; }
		[JsonProperty("callbackURL")]
		public string CallbackUrl { get; set; }
		[JsonProperty("active")]
		public bool? Active { get; set; }
	}
}