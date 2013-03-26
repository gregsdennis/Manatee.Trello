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
 
	File Name:		IRestResponse.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		IRestResponse<out T>
	Purpose:		Defines required properties returned by RESTful calls.

***************************************************************************************/
namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Defines required properties returned by RESTful calls.
	/// </summary>
	/// <typeparam name="T">The type expected to be returned by the call.</typeparam>
	public interface IRestResponse<T>
		where T : new()
	{
		/// <summary>
		/// The JSON content returned by the call.
		/// </summary>
		string Content { get; set; }
		/// <summary>
		/// The deserialized data.
		/// </summary>
		T Data { get; set; }
	}
}