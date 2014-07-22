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
 
	File Name:		EntityRequestType.cs
	Namespace:		Manatee.Trello.Enumerations.DataAccess
	Class Name:		EntityRequestType
	Purpose:		Enumerates the various types of requests, including which kind
					of entity is submitting the request and the desired operation.

***************************************************************************************/

namespace Manatee.Trello.Internal.DataAccess
{
	internal enum EntityRequestType
	{
#pragma warning disable 1591
		Unsupported,
		Action_Read_Refresh,
		//Action_Write_Delete,
		//Attachment_Read_Refresh,
		Attachment_Write_Delete,
		Board_Read_Actions,
		Board_Read_Cards,
		//Board_Read_CardsForMember,
		//Board_Read_Checklists,
		//Board_Read_InvitedMembers,
		Board_Read_Lists,
		Board_Read_Members,
		Board_Read_Memberships,
		Board_Read_Refresh,
		Board_Write_Update,
		Board_Write_AddList,
		//Board_Write_AddNewMember,
		Board_Write_AddOrUpdateMember,
		//Board_Write_Description,
		//Board_Write_InviteMember,
		//Board_Write_IsClosed,
		//Board_Write_IsPinned,
		//Board_Write_IsSubscribed,
		//Board_Write_MarkAsViewed,
		//Board_Write_Name,
		//Board_Write_Organization,
		Board_Write_RemoveMember,
		//Board_Write_RescindInvitation,
		BoardMembership_Read_Refresh,
		BoardMembership_Write_Update,
		//BoardPersonalPreferences_Read_Refresh,
		//BoardPersonalPreferences_Write_ShowListGuide,
		//BoardPersonalPreferences_Write_ShowSidebar,
		//BoardPersonalPreferences_Write_ShowSidebarActivity,
		//BoardPersonalPreferences_Write_ShowSidebarBoardActions,
		//BoardPersonalPreferences_Write_ShowSidebarMembers,
		//BoardPreferences_Read_Refresh,
		//BoardPreferences_Write_AllowsSelfJoin,
		//BoardPreferences_Write_Comments,
		//BoardPreferences_Write_Invitations,
		//BoardPreferences_Write_PermissionLevel,
		//BoardPreferences_Write_ShowCardCovers,
		//BoardPreferences_Write_Voting,
		Card_Read_Actions,
		Card_Read_Attachments,
		//Card_Read_CheckItems,
		//Card_Read_CheckLists,
		//Card_Read_Labels,
		Card_Read_Members,
		Card_Read_Refresh,
		//Card_Read_VotingMembers,
		Card_Write_Update,
		Card_Write_AddAttachment,
		Card_Write_AddChecklist,
		Card_Write_AddComment,
		//Card_Write_ApplyLabel,
		Card_Write_AssignMember,
		//Card_Write_ClearNotifications,
		Card_Write_Delete,
		//Card_Write_Description,
		//Card_Write_DueDate,
		//Card_Write_IsClosed,
		//Card_Write_IsSubscribed,
		//Card_Write_Move,
		//Card_Write_Name,
		//Card_Write_Position,
		//Card_Write_RemoveAttachment,
		//Card_Write_RemoveLabel,
		Card_Write_RemoveMember,
		//Card_Write_WarnWhenUpcoming,
		CheckItem_Read_Refresh,
		CheckItem_Write_Delete,
		//CheckItem_Write_Name,
		//CheckItem_Write_Position,
		//CheckItem_Write_State,
		CheckItem_Write_Update,
		//CheckList_Read_CheckItems,
		CheckList_Read_Refresh,
		CheckList_Write_AddCheckItem,
		//CheckList_Write_Card,
		CheckList_Write_Delete,
		//CheckList_Write_Name,
		//CheckList_Write_Position,
		CheckList_Write_Update,
		//Label_Read_Refresh,
		//LabelNames_Read_Refresh,
		//LabelNames_Write_Blue,
		//LabelNames_Write_Green,
		//LabelNames_Write_Orange,
		//LabelNames_Write_Purple,
		//LabelNames_Write_Red,
		//LabelNames_Write_Yellow,
		List_Read_Actions,
		List_Read_Cards,
		List_Read_Refresh,
		List_Write_AddCard,
		//List_Write_Board,
		//List_Write_Delete,
		//List_Write_IsClosed,
		//List_Write_IsSubscribed,
		//List_Write_Move,
		//List_Write_Name,
		//List_Write_Position,
		List_Write_Update,
		Member_Read_Actions,
		Member_Read_Boards,
		//Member_Read_Cards,
		//Member_Read_InvitedBoards,
		//Member_Read_InvitedOrganizations,
		//Member_Read_Notifications,
		Member_Read_Organizations,
		Member_Read_Refresh,
		//Member_Read_Sessions,
		//Member_Read_StarredBoards,
		//Member_Read_Tokens,
		//Member_Write_AvatarSource,
		//Member_Write_Bio,
		//Member_Write_ClearNotifications,
		Member_Write_CreateBoard,
		Member_Write_CreateOrganization,
		//Member_Write_FullName,
		//Member_Write_Initials,
		//Member_Write_PinBoard,
		//Member_Write_RescindVoteForCard,
		//Member_Write_UnpinBoard,
		Member_Write_Update,
		//Member_Write_Username,
		//Member_Write_VoteForCard,
		//MemberPreferences_Read_Refresh,
		//MemberPreferences_Write_ColorBlind,
		//MemberPreferences_Write_MinutesBeforeDeadlineToNotify,
		//MemberPreferences_Write_MinutesBetweenSummaries,
		//MemberPreferences_Write_SendSummaries,
		//MemberSession_Write_Delete,
		//Notification_Read_Refresh,
		//Notification_Write_IsUnread,
		Organization_Read_Actions,
		Organization_Read_Boards,
		//Organization_Read_InvitedMembers,
		Organization_Read_Members,
		Organization_Read_Memberships,
		Organization_Read_Refresh,
		Organization_Write_AddOrUpdateMember,
		Organization_Write_CreateBoard,
		Organization_Write_Delete,
		//Organization_Write_Description,
		//Organization_Write_DisplayName,
		//Organization_Write_InviteMember,
		//Organization_Write_Name,
		Organization_Write_RemoveMember,
		//Organization_Write_RescindInvitation,
		Organization_Write_Update,
		//Organization_Write_Website,
		OrganizationMembership_Read_Refresh,
		OrganizationMembership_Write_Update,
		//OrganizationPreferences_Read_Refresh,
		//OrganizationPreferences_Write_AssociatedDomain,
		//OrganizationPreferences_Write_ExternalMembersDisabled,
		//OrganizationPreferences_Write_OrgInviteRestrict,
		//OrganizationPreferences_Write_OrgVisibleBoardVisibility,
		//OrganizationPreferences_Write_PermissionLevel,
		//OrganizationPreferences_Write_PrivateBoardVisibility,
		//OrganizationPreferences_Write_PublicBoardVisibility,
		Service_Read_Me,
		Service_Read_Search,
		Service_Read_SearchMembers,
		Token_Read_Refresh,
		Token_Write_Delete,
		Webhook_Read_Refresh,
		//Webhook_Write_Active,
		//Webhook_Write_CallbackUrl,
		Webhook_Write_Delete,
		//Webhook_Write_Description,
		Webhook_Write_Entity,
		Webhook_Write_Update,
#pragma warning restore 1591
	}
}