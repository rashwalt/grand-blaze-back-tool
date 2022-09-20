using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 連携属性リスト
    /// </summary>
    public class LibChain
    {
        /// <summary>
        /// 切断
        /// </summary>
        private bool Slash = false;

        /// <summary>
        /// 貫通
        /// </summary>
        private bool Spike = false;

        /// <summary>
        /// 空烈
        /// </summary>
        private bool Swing = false;

        /// <summary>
        /// 炎熱
        /// </summary>
        private bool Frame = false;

        /// <summary>
        /// 硬化
        /// </summary>
        private bool Freeze = false;

        /// <summary>
        /// 衝撃
        /// </summary>
        private bool Shock = false;

        /// <summary>
        /// 振動
        /// </summary>
        private bool Vibrate = false;

        /// <summary>
        /// 収束
        /// </summary>
        private bool Converge = false;

        /// <summary>
        /// チェイン発生かどうか
        /// </summary>
        public bool IsChainStatus = false;

        public LibChain()
        {
            Slash = false;
            Spike = false;
            Swing = false;
            Frame = false;
            Freeze = false;
            Shock = false;
            Vibrate = false;
            Converge = false;
        }

        /// <summary>
        /// 連携属性追加＆発動効果選定
        /// </summary>
        public int Add(bool SlashAdd, bool SpikeAdd, bool SwingAdd, bool FrameAdd, bool FreezeAdd, bool ShockAdd, bool VibrateAdd, bool ConvergeAdd)
        {
            // 発生追加アーツ種類

            if (Swing && SlashAdd)
            {
                Clear();

                return Status.ChainArts.DoubleBlade;
            }
            else if (Slash && ConvergeAdd)
            {
                Clear();

                return Status.ChainArts.VariantGuard;
            }
            else if (Freeze && VibrateAdd)
            {
                Clear();

                return Status.ChainArts.StunBrow;
            }
            else if (Frame && ShockAdd)
            {
                Clear();

                return Status.ChainArts.PowerdFist;
            }
            else if (Converge && SpikeAdd)
            {
                Clear();

                return Status.ChainArts.SwiflyMorgan;
            }
            else if (Spike && FreezeAdd)
            {
                Clear();

                return Status.ChainArts.AstralForce;
            }
            else if (Vibrate && FrameAdd)
            {
                Clear();

                return Status.ChainArts.BlackGravity;
            }
            else if (Shock && SwingAdd)
            {
                Clear();

                return Status.ChainArts.WhiteOut;
            }

            Slash = SlashAdd;
            Spike = SpikeAdd;
            Swing = SwingAdd;
            Frame = FrameAdd;
            Freeze = FreezeAdd;
            Shock = ShockAdd;
            Vibrate = VibrateAdd;
            Converge = ConvergeAdd;

            IsChainStatus = true;

            return Status.ChainArts.None;
        }

        /// <summary>
        /// 属性リセット
        /// </summary>
        public void Clear()
        {
            Slash = false;
            Spike = false;
            Swing = false;
            Frame = false;
            Freeze = false;
            Shock = false;
            Vibrate = false;
            Converge = false;
            IsChainStatus = false;
        }
    }
}
