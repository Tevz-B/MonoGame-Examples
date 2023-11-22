using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Express.Scene;

public interface IScene : IList<object>, IUpdateable
{
    int UpdateOrder { set; }
}