﻿using System;

namespace Marketplace.Common.Application.Commands
{
    public abstract class InternalCommandBase : ICommand
    {

        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {

        protected InternalCommandBase()
        {
            Id = Guid.NewGuid();
        }

        protected InternalCommandBase(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
