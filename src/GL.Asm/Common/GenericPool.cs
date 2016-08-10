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
 * along with this program.
 * If not, see <https://www.gnu.org/licenses/gpl-3.0.html>.
 **/

using System;
using System.Collections.Generic;

namespace GL.Asm.Common
{
    public abstract class GenericPool<T>
    {
        #region Fields

        private List<T> available;
        private List<T> inUse;

        #endregion

        #region Constructors

        public GenericPool(int maxCapacity, int initialCapacity)
        {
            this.available = new List<T>(initialCapacity);
            this.inUse = new List<T>(initialCapacity);
        }

        public GenericPool(int maxCapacity)
            : this(maxCapacity, maxCapacity) { }

        #endregion

            #region Methods

        public T Pop()
        {
            lock (available)
            {
                if (this.available.Count != 0)
                {
                    T obj = this.available[0];
                    this.inUse.Add(obj);
                    this.available.RemoveAt(0);
                    return obj;
                }
                else
                {
                    if (this.available.Count + this.inUse.Count >= Max) { throw new InvalidOperationException("The pool has reached its maximal capacity : " + Max); }
                    T obj = Create();
                    this.inUse.Add(obj);
                    return obj;
                }
            }
        }

        public void Push(T obj)
        {
            CleanUp(obj);

            lock (this.available)
            {
                this.available.Add(obj);
                this.inUse.Remove(obj);
            }
        }

        protected abstract void CleanUp(T obj);

        protected abstract T Create();

        #endregion

        #region Properties

        public int Max { get; private set; }

        public int Available { get { lock (this.available) { return this.available.Count; } } }

        public int InUse { get { lock (this.available) { return this.inUse.Count; } } }

        #endregion
    }
}
