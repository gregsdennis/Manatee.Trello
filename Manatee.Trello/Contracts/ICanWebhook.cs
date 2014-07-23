/***************************************************************************************

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
 
	File Name:		ICanWebhook.cs
	Namespace:		Manatee.Trello.Contracts
	Class Name:		ICanWebhook
	Purpose:		Definines properties and methods required to support
					webhooks.

***************************************************************************************/
namespace Manatee.Trello.Contracts
{
	/// <summary>
	/// Definines properties and methods required to support webhooks.
	/// </summary>
	public interface ICanWebhook
	{
		/// <summary>
		/// Gets the ID.
		/// </summary>
		string Id { get; }
		/// <summary>
		/// Applies the changes an action represents.
		/// </summary>
		/// <param name="action">The action.</param>
		void ApplyAction(Action action);
	}
}