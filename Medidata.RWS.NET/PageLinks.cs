using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Medidata.RWS
{

    /// <summary>
    /// Used to determine the links to other pages of request
    /// responses encoded in the current response.These will be present if the
    /// result set size exceeds the per page limit.
    ///  
    /// Based on
    /// https://github.com/eclipse/egit-github/blob/master/org.eclipse.egit.github.core/src/org/eclipse/egit/github/core/client/PageLinks.java#L43-75
    /// </summary>
    public class PageLinks
    {

        private const char DELIM_LINKS = ','; //$NON-NLS-1$

	    private const char DELIM_LINK_PARAM = ';'; //$NON-NLS-1$

	    private string first;
        private string last;
        private string next;
        private string prev;

        /// <summary>
        /// Parse links contained in the header of an IRestResponse object.
        /// </summary>
        /// <param name="response"></param>
        public PageLinks(IRestResponse response)
        {

            if (response == null) return;

            if (response.Headers.Any(h => h.Name == Constants.HEADER_LINK))
            {

                string linkHeader = response.Headers.FirstOrDefault(t => t.Name == Constants.HEADER_LINK).Value.ToString();

                List<string> links = linkHeader.Split(DELIM_LINKS).ToList();


                foreach (string link in links)
                {
                    List<string> segments = link.Split(DELIM_LINK_PARAM).ToList();
                    if (segments.Count < 2)
                        continue;

                    string linkPart = segments.First().Trim();

                    if (!linkPart.StartsWith("<") || !linkPart.EndsWith(">")) //$NON-NLS-1$ //$NON-NLS-2$
                        continue;

                    linkPart = linkPart.TrimStart('<').TrimEnd('>');

                    foreach (string segment in segments)
                    {

                        List<string> rel = segment.Trim().Split('=').ToList();

                        if (rel.Count < 2 || !Constants.META_REL.Equals(rel.First()))
                            continue;

                        string relValue = rel.ElementAt(1);

                        if (relValue.StartsWith(@"""") && relValue.EndsWith(@"""")) //$NON-NLS-1$ //$NON-NLS-2$
                            relValue = relValue.TrimStart('"').TrimEnd('"');

                        if (Constants.META_FIRST.Equals(relValue))
                            first = linkPart;
                        else if (Constants.META_LAST.Equals(relValue))
                            last = linkPart;
                        else if (Constants.META_NEXT.Equals(relValue))
                            next = linkPart;
                        else if (Constants.META_PREV.Equals(relValue))
                            prev = linkPart;
                    }


                }

            }
            else
            {

                next = response.Headers.FirstOrDefault(t => t.Name == Constants.HEADER_NEXT) == null ? "" : response.Headers.FirstOrDefault(t => t.Name == Constants.HEADER_NEXT).Value.ToString();
                last = response.Headers.FirstOrDefault(t => t.Name == Constants.HEADER_LAST) == null ? "" : response.Headers.FirstOrDefault(t => t.Name == Constants.HEADER_LAST).Value.ToString();
               
            }

        }


        /// <summary>
        /// Get the first page.
        /// </summary>
        /// <returns></returns>
        public string GetFirst()
        {
            return first;
        }

        /// <summary>
        /// Get the last page.
        /// </summary>
        /// <returns></returns>
        public string GetLast()
        {
            return last;
        }

        /// <summary>
        /// Get the next page.
        /// </summary>
        /// <returns></returns>
        public string GetNext()
        {
            return next;
        }

        /// <summary>
        /// Get the previous page.
        /// </summary>
        /// <returns></returns>
        public string GetPrevious()
        {
            return prev;
        }

    }
}
