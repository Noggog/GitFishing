﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingWithGit
{
    public class PushHooks : HookSet
    {
        PushArgs args;
        public override IGitHookArgs Args => args;

        private PushHooks(BaseWrapper wrapper, List<string> args)
            : base(wrapper)
        {
            this.args = new PushArgs(args.ToArray());
        }

        public static HookSet Factory(BaseWrapper wrapper, List<string> args, int commandIndex)
        {
            return new PushHooks(wrapper, args);
        }

        public override Task<int> PreCommand()
        {
            return CommonFunctions.RunCommands(
                () => this.Wrapper.FireAllHooks(HookType.Pre_Push, HookLocation.InRepo, args.ToArray()),
                () => this.Wrapper.FireUnnaturalHooks(HookType.Pre_Push, HookLocation.Normal, args.ToArray()));
        }

        public override Task<int> PostCommand()
        {
            return CommonFunctions.RunCommands(
                () => this.Wrapper.FireAllHooks(HookType.Post_Push, HookLocation.InRepo, args.ToArray()),
                () => this.Wrapper.FireAllHooks(HookType.Post_Push, HookLocation.Normal, args.ToArray()));
        }
    }
}
