using osu.Framework.Testing;

namespace OFDepaumer.Game.Tests.Visual
{
    public class OFDepaumerTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new OFDepaumerTestSceneTestRunner();

        private class OFDepaumerTestSceneTestRunner : OFDepaumerGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}
