﻿// <copyright company="Fireasy"
//      email="faib920@126.com"
//      qq="55570729">
//   (c) Copyright Fireasy. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Fireasy.Common
{
    /// <summary>
    /// 实现了标准的 <see cref="IDisposable"/> 模式的抽象类。
    /// </summary>
    public abstract class DisposeableBase : IDisposable
#if NETSTANDARD2_1
        , IAsyncDisposable
#endif
    {
        private bool isDisposed = false;

        /// <summary>
        /// 获取是否检验是否已经释放，当为 true 时，重复 Dispose 会引发 <see cref="ObjectDisposedException"/> 异常。默认为 false。
        /// </summary>
        public virtual bool VerifyDisposed { get; }

        [SuppressMessage("Design", "CA1063")]
        ~DisposeableBase()
        {
            DoDispose(false);
        }

        /// <summary>
        /// 释放对象所占用的非托管和托管资源。
        /// </summary>
        /// <param name="disposing">为 true 则释放托管资源和非托管资源；为 false 则仅释放非托管资源。</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        private void DoDispose(bool disposing)
        {
            if (VerifyDisposed && isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (!isDisposed)
            {
                Dispose(disposing);

                isDisposed = true;
            }
        }

        /// <summary>
        /// 手动释放非托管资源。
        /// </summary>
        [SuppressMessage("Design", "CA1063")]
        public void Dispose()
        {
            DoDispose(true);
            GC.SuppressFinalize(this);
        }

#if NETSTANDARD2_1
        /// <summary>
        /// 异步的，释放对象所占用的非托管和托管资源。
        /// </summary>
        /// <param name="disposing">为 true 则释放托管资源和非托管资源；为 false 则仅释放非托管资源。</param>
        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
        }

        private async ValueTask DoDisposeAsync(bool disposing)
        {
            if (VerifyDisposed && isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }

            if (!isDisposed)
            {
                await DisposeAsync(disposing);

                isDisposed = true;
            }
        }

        /// <summary>
        /// 异步的，手动释放非托管资源。
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            await DoDisposeAsync(true);
            GC.SuppressFinalize(this);
        }
#endif
    }
}