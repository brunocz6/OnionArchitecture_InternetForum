﻿using System.Collections.Generic;
using System.Linq;

namespace InternetForum.Application.Common.Models
{
    /// <summary>
    /// Třída obsahující výsledek nějaké obecné operace.
    /// </summary>
    public class Result
    {
        internal Result(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }

        public string[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Failure(IEnumerable<string> errors)
        {
            return new Result(false, errors);
        }

    }
}
