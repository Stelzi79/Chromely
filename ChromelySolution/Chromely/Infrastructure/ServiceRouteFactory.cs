﻿/**
 MIT License

 Copyright (c) 2017 Kola Oyewumi

 Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

 The above copyright notice and this permission notice shall be included in all
 copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 SOFTWARE.
 */

namespace Chromely.Infrastructure
{
    using Chromely.RestfulService;
    using System;
    using System.Collections.Generic;

    public static class ServiceRouteFactory
    {
        private static Dictionary<string, Route> RouteDictionary = new Dictionary<string, Route>();
        private static object m_lockThis = new object();

        public static void AddRoute(string key, Route route)
        {
            RouteDictionary.Add(key, route);
        }

        public static Route GetRoute(string routePath)
        {
            if (!RouteDictionary.ContainsKey(routePath))
            {
                throw new Exception(string.Format("No route found for route path = {0}.", routePath));
            }

            return RouteDictionary[routePath];
        }

        public static void MergeRoutes(Dictionary<string, Route> newRouteDictionary)
        {
            lock (m_lockThis)
            {
                if ((newRouteDictionary != null) && (newRouteDictionary.Count > 0))
                {
                    foreach (var item in newRouteDictionary)
                    {
                        if (!RouteDictionary.ContainsKey(item.Key))
                        {
                            RouteDictionary.Add(item.Key, item.Value);
                        }
                    }
                }
            }
        }
    }
}