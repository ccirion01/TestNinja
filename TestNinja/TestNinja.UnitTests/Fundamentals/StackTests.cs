using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        /*Push
         * object is null -> exception
         * object is not null ->  count should be 1?
         * 
         * Pop
         * list is empty -> exception
         * list is not empty -> should return the last element and remove it
         * 
         * Peek
         * list is empty -> exception
         * list is not empty -> should return the last element without removing it
         */
        [Test]
        public void Push_ArgumentIsNull_ThrowArgumentNullException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_ValidArgument_AddArgumentToTheStack()
        {
            var stack = new Stack<string>();

            stack.Push("abc");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<string>();

            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        //NOTE!: Here we should have two separate tests.
        //[Test]
        //public void Pop_StackWithElements_RemoveAndReturnLastElementOfStack()
        //{        
        //}

        [Test]
        public void Pop_StackWithElements_ReturnTopElementOfStack()
        {
            //Arrange
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            //Act
            var result =  stack.Pop();

            //Assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Pop_StackWithElements_RemoveTopElementOfStack()
        {
            //Arrange
            var stack = new Stack<string>();
            stack.Push("a");
            var initialCount = stack.Count;

            //Act
            stack.Pop();

            //Assert
            Assert.That(stack.Count, Is.EqualTo(initialCount - 1));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackWithElements_ReturnTopElementOfStack()
        {
            //Arrange
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            //Act
            var result = stack.Peek();

            //Assert
            Assert.That(result, Is.EqualTo("c"));
        }

        [Test]
        public void Peek_StackWithElements_NotRemoveTopElementOfStack()
        {
            //Arrange
            var stack = new Stack<string>();
            stack.Push("a");
            var initialCount = stack.Count;

            //Act
            stack.Peek();

            //Assert
            Assert.That(stack.Count, Is.EqualTo(initialCount));
        }
    }
}
