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
 
	File Name:		MakeAdminOfBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		MakeAdminOfBoardAction
	Purpose:		Indicates a member was added to a board with admin
					permissions.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a member was added to a board with admin permissions.
	/// </summary>
	public class MakeAdminOfBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Member _member;
		private readonly string _memberId;

		/// <summary>
		/// Gets the board associated with the action.
		/// </summary>
		public Board Board
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_board == null) || (_board.Id != _boardId)) && (Svc != null) ? (_board = Svc.Retrieve<Board>(_boardId)) : _board;
			}
		}
		/// <summary>
		/// Gets the member associated with the action.
		/// </summary>
		public Member Member
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_member == null) || (_member.Id != _memberId)) && (Svc != null) ? (_member = Svc.Retrieve<Member>(_memberId)) : _member;
			}
		}

		/// <summary>
		/// Creates a new instance of the MakeAdminOfBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public MakeAdminOfBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board","id");
			_memberId = action.Data.TryGetString("idMember");
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>
		/// A string that represents the current object.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override string ToString()
		{
			return string.Format("{0} made {1} a admin of board '{2}' on {3}",
								 MemberCreator.FullName, Member.FullName, Board.Name, Date);
		}
	}
}