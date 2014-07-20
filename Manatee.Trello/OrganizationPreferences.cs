﻿/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		OrganizationPreferences.cs
	Namespace:		Manatee.Trello
	Class Name:		OrganizationPreferences
	Purpose:		Represents preferences for an organization.

***************************************************************************************/

using Manatee.Trello.Enumerations;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;

namespace Manatee.Trello
{
	public class OrganizationPreferences
	{
		private readonly Field<OrganizationPermissionLevel> _permissionLevel;
		private readonly Field<bool?> _externalMembersDisabled;
		private readonly Field<string> _assocatedDomain;
		private readonly Field<OrganizationBoardVisibility> _publicBoardVisibility;
		private readonly Field<OrganizationBoardVisibility> _organizationBoardVisibility;
		private readonly Field<OrganizationBoardVisibility> _privateBoardVisibility;
		private OrganizationPreferencesContext _context;

		public OrganizationPermissionLevel PermissionLevel
		{
			get { return _permissionLevel.Value; }
			set { _permissionLevel.Value = value; }
		}
		public bool? ExternalMembersDisabled
		{
			get { return _externalMembersDisabled.Value; }
			set { _externalMembersDisabled.Value = value; }
		}
		public string AssociatedDomain
		{
			get { return _assocatedDomain.Value; }
			set { _assocatedDomain.Value = value; }
		}
		public OrganizationBoardVisibility PublicBoardVisibility
		{
			get { return _publicBoardVisibility.Value; }
			set { _publicBoardVisibility.Value = value; }
		}
		public OrganizationBoardVisibility OrganizationBoardVisibility
		{
			get { return _organizationBoardVisibility.Value; }
			set { _organizationBoardVisibility.Value = value; }
		}
		public OrganizationBoardVisibility PrivateBoardVisibility
		{
			get { return _privateBoardVisibility.Value; }
			set { _privateBoardVisibility.Value = value; }
		}

		internal OrganizationPreferences(OrganizationPreferencesContext context)
		{
			_context = context;

			_permissionLevel = new Field<OrganizationPermissionLevel>(_context, () => PermissionLevel);
			_permissionLevel.AddRule(EnumerationRule<OrganizationPermissionLevel>.Instance);
			_externalMembersDisabled = new Field<bool?>(_context, () => ExternalMembersDisabled);
			_externalMembersDisabled.AddRule(NullableHasValueRule<bool>.Instance);
			_assocatedDomain = new Field<string>(_context, () => AssociatedDomain);
			_publicBoardVisibility = new Field<OrganizationBoardVisibility>(_context, () => PublicBoardVisibility);
			_publicBoardVisibility.AddRule(EnumerationRule<OrganizationBoardVisibility>.Instance);
			_organizationBoardVisibility = new Field<OrganizationBoardVisibility>(_context, () => OrganizationBoardVisibility);
			_organizationBoardVisibility.AddRule(EnumerationRule<OrganizationBoardVisibility>.Instance);
			_privateBoardVisibility = new Field<OrganizationBoardVisibility>(_context, () => PrivateBoardVisibility);
			_privateBoardVisibility.AddRule(EnumerationRule<OrganizationBoardVisibility>.Instance);
		}
	}
}