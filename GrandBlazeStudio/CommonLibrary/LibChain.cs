using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// �A�g�������X�g
    /// </summary>
    public class LibChain
    {
        /// <summary>
        /// �ؒf
        /// </summary>
        private bool Slash = false;

        /// <summary>
        /// �ђ�
        /// </summary>
        private bool Spike = false;

        /// <summary>
        /// ���
        /// </summary>
        private bool Swing = false;

        /// <summary>
        /// ���M
        /// </summary>
        private bool Frame = false;

        /// <summary>
        /// �d��
        /// </summary>
        private bool Freeze = false;

        /// <summary>
        /// �Ռ�
        /// </summary>
        private bool Shock = false;

        /// <summary>
        /// �U��
        /// </summary>
        private bool Vibrate = false;

        /// <summary>
        /// ����
        /// </summary>
        private bool Converge = false;

        /// <summary>
        /// �`�F�C���������ǂ���
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
        /// �A�g�����ǉ����������ʑI��
        /// </summary>
        public int Add(bool SlashAdd, bool SpikeAdd, bool SwingAdd, bool FrameAdd, bool FreezeAdd, bool ShockAdd, bool VibrateAdd, bool ConvergeAdd)
        {
            // �����ǉ��A�[�c���

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
        /// �������Z�b�g
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
