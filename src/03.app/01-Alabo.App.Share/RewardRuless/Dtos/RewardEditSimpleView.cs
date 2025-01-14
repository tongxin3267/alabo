﻿using Alabo.UI.Design.AutoForms;
using System;

namespace Alabo.App.Share.RewardRuless.Dtos
{
    public class RewardEditSimpleView
    {
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     自动表单
        /// </summary>
        public AutoForm AutoForm { get; set; }
    }
}