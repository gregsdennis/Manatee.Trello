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
 
	File Name:		MoveListFromBoardAction.cs
	Namespace:		Manatee.Trello
	Class Name:		MoveListFromBoardAction
	Purpose:		Indicates a list was removed from a board.

***************************************************************************************/
namespace Manatee.Trello
{
	/// <summary>
	/// Indicates a list was removed from a board.
	/// </summary>
	public class MoveListFromBoardAction : Action
	{
		private Board _board;
		private readonly string _boardId;
		private Board _boardTarget;
		private readonly string _boardTargetId;
		private List _list;
		private readonly string _listId;
		private readonly string _boardName;
		private readonly string _boardTargetName;
		private readonly string _listName;

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
		/// Gets the board associated with the action.
		/// </summary>
		public Board BoardTarget
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				return ((_boardTarget == null) || (_boardTarget.Id != _boardTargetId)) && (Svc != null) ? (_boardTarget = Svc.Retrieve<Board>(_boardTargetId)) : _boardTarget;
			}
		}
		/// <summary>
		/// Gets the list associated with the action.
		/// </summary>
		public List List
		{
			get
			{
				if (_isDeleted) return null;
				VerifyNotExpired();
				if (_listId == null) return null;
				return ((_list == null) || (_list.Id != _listId)) && (Svc != null) ? (_list = Svc.Retrieve<List>(_listId)) : _list;
			}
		}

		/// <summary>
		/// Creates a new instance of the MoveListFromBoardAction class.
		/// </summary>
		/// <param name="action">The base action</param>
		public MoveListFromBoardAction(Action action)
			: base(action.Svc, action.Id)
		{
			VerifyNotExpired();
			_boardId = action.Data.TryGetString("board","id");
			_boardName = action.Data.TryGetString("board","name");
			_boardTargetId = action.Data.TryGetString("boardTarget","id");
			_boardTargetName = action.Data.TryGetString("boardTarget","name");
			_listId = action.Data.TryGetString("list","id");
			_listName = action.Data.TryGetString("list","name");
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
			return string.Format("{0} moved list '{1}' from board '{2}' to board '{3}' on {4}",
								 MemberCreator.FullName,
								 List != null ? List.Name : _listName,
								 Board != null ? Board.Name : _boardName,
								 BoardTarget != null ? BoardTarget.Name : _boardTargetName,
								 Date);
		}
	}
}