﻿using System.Collections.Generic;
using Common.Interfaces;

namespace Common.Renders
{
	public class PathFindingRender : IRenderState
	{
		public IEnumerable<INode> Set { get; set; }
	}
}
