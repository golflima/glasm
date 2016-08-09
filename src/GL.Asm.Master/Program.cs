/**
 * Copyright (C) 2016, Jérémy WALTHER <jeremy.walther@golflima.net>
 * 
 * This file is part of Glasm (GL.Asm).
 * 
 * Glasm - GL's Application and System Manager
 * Provides solutions to administrate remote systems and applications.
 * <https://github.com/golflima/glasm>
 *
 * Glasm is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Glasm is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 **/

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using GL.Asm.Net;

namespace GL.Asm.Master
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new AsyncServer(15, 1024).Init().Start(new IPEndPoint(IPAddress.Any, GlasmConsts.DefaultPort));
        }
    }
}