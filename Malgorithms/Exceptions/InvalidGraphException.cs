// <copyright file="InvalidGraphException.cs">
// Copyright (c) Ben Thomas Meysner. All rights reserved.
// </copyright>

namespace Malgorithms.Exceptions
{
    using System;

    /// <summary>
    /// Malgorithms.Exceptions.InvalidGraphException
    /// </summary>
    public class InvalidGraphException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidGraphException"/> class.
        /// </summary>
        public InvalidGraphException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidGraphException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidGraphException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidGraphException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public InvalidGraphException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
