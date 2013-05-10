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
 
	File Name:		PremiumOrganization.cs
	Namespace:		Manatee.Trello.Internal
	Class Name:		PremiumOrganization
	Purpose:		Represents a single premium organization on Trello.com.

***************************************************************************************/
using System;

namespace Manatee.Trello.Internal
{
	internal class PremiumOrganization : Organization, IEquatable<PremiumOrganization>
	{
		internal new static string TypeKey { get { return "idPremOrgsAdmin"; } }
		internal override string Key { get { return TypeKey; } }

		public bool Equals(PremiumOrganization other)
		{
			return base.Equals(this);
		}
	}
}