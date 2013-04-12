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
 
	File Name:		Action.cs
	Namespace:		Manatee.Trello
	Class Name:		Action
	Purpose:		Represents an action on Trello.com.

***************************************************************************************/
using System;
using System.Linq;
using Manatee.Json;
using Manatee.Json.Enumerations;
using Manatee.Json.Extensions;
using Manatee.Trello.Contracts;
using Manatee.Trello.Implementation;

namespace Manatee.Trello
{
	//{
	//  "id":"5144719b047913c06e00294d",
	//  "idMemberCreator":"50b693ad6f122b4310000a3c",
	//  "data":{
	//     "board":{
	//        "name":"Manatee.Json",
	//        "id":"50d227239c7b29575f000f99"
	//     },
	//     "idMember":"514464db3fa062da6e00254f"
	//  },
	//  "type":"makeNormalMemberOfBoard",
	//  "date":"2013-03-16T13:20:27.315Z",
	//  "member":{
	//     "id":"514464db3fa062da6e00254f",
	//     "avatarHash":null,
	//     "fullName":"Little Crab Solutions",
	//     "initials":"LS",
	//     "username":"s_littlecrabsolutions"
	//  },
	//  "memberCreator":{
	//     "id":"50b693ad6f122b4310000a3c",
	//     "avatarHash":"e97c40e0d0b85ab66661dbff5082d627",
	//     "fullName":"Greg Dennis",
	//     "initials":"GD",
	//     "username":"gregsdennis"
	//  }
	//}
	/// <summary>
	/// Actions are generated by Trello to record what users do.
	/// </summary>
	public class Action : JsonCompatibleExpiringObject, IEquatable<Action>
	{
		private static readonly OneToOneMap<ActionType, string> _typeMap;

		private string _apiType;
		private string _memberCreatorId;
		private Member _memberCreator;
		private JsonValue _data;
		private ActionType _type = ActionType.Unknown;
		private DateTime? _date;

		/// <summary>
		/// The member who performed the action.
		/// </summary>
		public Member MemberCreator
		{
			get
			{
				return ((_memberCreator == null) || (_memberCreator.Id != _memberCreatorId)) && (Svc != null)
				       	? (_memberCreator = Svc.Get(Svc.RequestProvider.Create<Member>(_memberCreatorId)))
				       	: _memberCreator;
			}
		}
		/// <summary>
		/// Data associated with the action.  Contents depend upon the action's type.
		/// </summary>
		internal JsonValue Data { get { return _data; } set { _data = value; } }
		/// <summary>
		/// The type of action performed.
		/// </summary>
		public ActionType Type { get { return _type; } internal set { _type = value; } }
		/// <summary>
		/// When the action was performed.
		/// </summary>
		public DateTime? Date { get { return _date; } }

		internal override string Key { get { return "actions"; } }

		static Action()
		{
			_typeMap = new OneToOneMap<ActionType, string>
			           	{
			           		{ActionType.AddAttachmentToCard, "addAttachmentToCard"},
			           		{ActionType.AddChecklistToCard, "addChecklistToCard"},
			           		{ActionType.AddMemberToBoard, "addMemberToBoard"},
			           		{ActionType.AddMemberToCard, "addMemberToCard"},
			           		{ActionType.AddMemberToOrganization, "addMemberToOrganization"},
			           		{ActionType.AddToOrganizationBoard, "addToOrganizationBoard"},
			           		{ActionType.CommentCard, "commentCard"},
			           		{ActionType.CopyCommentCard, "copyCommentCard"},
			           		{ActionType.ConvertToCardFromCheckItem, "convertToCardFromCheckItem"},
			           		{ActionType.CopyBoard, "copyBoard"},
			           		{ActionType.CreateBoard, "createBoard"},
			           		{ActionType.CreateCard, "createCard"},
			           		{ActionType.CopyCard, "copyCard"},
			           		{ActionType.CreateList, "createList"},
			           		{ActionType.CreateOrganization, "createOrganization"},
			           		{ActionType.DeleteAttachmentFromCard, "deleteAttachmentFromCard"},
			           		{ActionType.DeleteBoardInvitation, "deleteBoardInvitation"},
			           		{ActionType.DeleteOrganizationInvitation, "deleteOrganizationInvitation"},
			           		{ActionType.MakeAdminOfBoard, "makeAdminOfBoard"},
			           		{ActionType.MakeNormalMemberOfBoard, "makeNormalMemberOfBoard"},
			           		{ActionType.MakeNormalMemberOfOrganization, "makeNormalMemberOfOrganization"},
			           		{ActionType.MakeObserverOfBoard, "makeObserverOfBoard"},
			           		{ActionType.MemberJoinedTrello, "memberJoinedTrello"},
			           		{ActionType.MoveCardFromBoard, "moveCardFromBoard"},
			           		{ActionType.MoveListFromBoard, "moveListFromBoard"},
			           		{ActionType.MoveCardToBoard, "moveCardToBoard"},
			           		{ActionType.MoveListToBoard, "moveListToBoard"},
			           		{ActionType.RemoveAdminFromBoard, "removeAdminFromBoard"},
			           		{ActionType.RemoveAdminFromOrganization, "removeAdminFromOrganization"},
			           		{ActionType.RemoveChecklistFromCard, "removeChecklistFromCard"},
			           		{ActionType.RemoveFromOrganizationBoard, "removeFromOrganizationBoard"},
			           		{ActionType.RemoveMemberFromCard, "removeMemberFromCard"},
			           		{ActionType.UnconfirmedBoardInvitation, "unconfirmedBoardInvitation"},
			           		{ActionType.UnconfirmedOrganizationInvitation, "unconfirmedOrganizationInvitation"},
			           		{ActionType.UpdateBoard, "updateBoard"},
			           		{ActionType.UpdateCard, "updateCard"},
			           		{ActionType.UpdateCheckItemStateOnCard, "updateCheckItemStateOnCard"},
			           		{ActionType.UpdateChecklist, "updateChecklist"},
			           		{ActionType.UpdateMember, "updateMember"},
			           		{ActionType.UpdateOrganization, "updateOrganization"},
			           		{ActionType.UpdateCardIdList, "updateCard:idList"},
			           		{ActionType.UpdateCardClosed, "updateCard:closed"},
			           		{ActionType.UpdateCardDesc, "updateCard:desc"},
			           		{ActionType.UpdateCardName, "updateCard:name"},
			           	};
		}
		///<summary>
		/// Creates a new instance of the Action class.
		///</summary>
		public Action() {}
		internal Action(ITrelloRest svc, string id)
			: base(svc, id) {}

		/// <summary>
		/// Deletes this action.  This cannot be undone.
		/// </summary>
		public void Delete()
		{
			if (Svc == null) return;
			Svc.Delete(Svc.RequestProvider.Create<Action>(Id));
		}
		/// <summary>
		/// Builds an object from a JsonValue.
		/// </summary>
		/// <param name="json">The JsonValue representation of the object.</param>
		public override void FromJson(JsonValue json)
		{
			if (json == null) return;
			if (json.Type != JsonValueType.Object) return;
			var obj = json.Object;
			Id = obj.TryGetString("id");
			_apiType = obj.TryGetString("type");
			_data = obj.TryGetObject("data");
			var date = obj.TryGetString("date");
			_date = string.IsNullOrWhiteSpace(date) ? (DateTime?) null : DateTime.Parse(date);
			_memberCreatorId = obj.TryGetString("idCreatorMember");
			UpdateType();
			_isInitialized = true;
		}
		/// <summary>
		/// Converts an object to a JsonValue.
		/// </summary>
		/// <returns>
		/// The JsonValue representation of the object.
		/// </returns>
		public override JsonValue ToJson()
		{
			if (!_isInitialized) VerifyNotExpired();
			var json = new JsonObject
			           	{
							{"id", Id},
			           		{"type", _apiType},
			           		{"data", _data ?? JsonValue.Null},
			           		{"date", _date.HasValue ? _date.Value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : JsonValue.Null},
			           		{"idCreatorMember", _memberCreatorId},
			           	};
			return json;
		}
		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Action other)
		{
			return Id == other.Id;
		}
		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <param name="obj">The object to compare with the current object. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (!(obj is Action)) return false;
			return Equals((Action) obj);
		}
		/// <summary>
		/// Serves as a hash function for a particular type. 
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object"/>.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal override void Refresh(ExpiringObject entity)
		{
			var action = entity as Action;
			if (action == null) return;
			_apiType = action._apiType;
			_data = action._data;
			_date = action._date;
			_memberCreatorId = action._memberCreatorId;
			UpdateType();
			_isInitialized = true;
		}

		/// <summary>
		/// Retrieves updated data from the service instance and refreshes the object.
		/// </summary>
		protected override void Get()
		{
			var entity = Svc.Get(Svc.RequestProvider.Create<Action>(Id));
			Refresh(entity);
		}
		/// <summary>
		/// Propigates the service instance to the object's owned objects.
		/// </summary>
		protected override void PropigateService()
		{
			if (_memberCreator != null) _memberCreator.Svc = Svc;
		}

		private void UpdateType()
		{
			_type = _typeMap.Any(kvp => kvp.Value == _apiType) ? _typeMap[_apiType] : ActionType.Unknown;
		}
		private void UpdateApiType()
		{
			if (_typeMap.Any(kvp => kvp.Key == _type))
				_apiType = _typeMap[_type];
		}
	}
}
