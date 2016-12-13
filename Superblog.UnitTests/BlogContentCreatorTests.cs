using System;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperBlog.Domain.Abstract;
using SuperBlog.Domain.Concrete;
using SuperBlog.Domain.Entities;

namespace Superblog.UnitTests
{
    [TestClass]
    public class BlogContentCreatorTests
    {
        [TestMethod]
        public void Can_Create_Post()
        {
            //Arrange
            Mock<IPostRepository> postRepositoryMock = new Mock<IPostRepository>();
            Mock<IUrlManager> urlManagerMock = new Mock<IUrlManager>();
            string mUrl = "mock url";
            urlManagerMock.Setup(m => m.GetEditUrl(It.IsAny<Post>())).Returns(mUrl);
            Mock<IModerationProcessor> moderationProcessorMock = new Mock<IModerationProcessor>();

            BlogContentCreator target = new BlogContentCreator(urlManagerMock.Object,
                moderationProcessorMock.Object);
            var p = new Post
            {
                Title = "Test Post",
                Text = "test, test, test!"
            };

            //Act
            var result = target.CreatePost(p, postRepositoryMock.Object);

            //Assert
            postRepositoryMock.Verify(m => m.SavePost(It.IsAny<Post>()));//some post is saved
            postRepositoryMock.Verify(m => m.SavePost(p));//our post is saved
            urlManagerMock.Verify(m => m.GetEditUrl(p));//edit url is retrieved
            moderationProcessorMock.Verify(m => m.ProcessModeration(p.ToString(), mUrl));//reported to moderator (e.g. e-mail is sended)
            Assert.AreEqual(result, (int)PostCreateReturns.Success);

        }

        [TestMethod]
        public void Can_Create_Category()
        {
            //Arrange
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            Mock<IUrlManager> urlManagerMock = new Mock<IUrlManager>();
            string mUrl = "mock url";
            urlManagerMock.Setup(m => m.GetEditUrl(It.IsAny<Category>())).Returns(mUrl);
            Mock<IModerationProcessor> moderationProcessorMock = new Mock<IModerationProcessor>();

            BlogContentCreator target = new BlogContentCreator(urlManagerMock.Object,
                moderationProcessorMock.Object);
            var category = new Category
            {
                Name = "mock name"
            };

            //Act
            var result = target.CreateCategory(category, categoryRepositoryMock.Object);

            //Assert
            categoryRepositoryMock.Verify(m => m.SaveCategory(It.IsAny<Category>()));//some category is saved
            categoryRepositoryMock.Verify(m => m.SaveCategory(category));//our category is saved
            urlManagerMock.Verify(m => m.GetEditUrl(category));//edit url is retrieved
            moderationProcessorMock.Verify(m => m.ProcessModeration(category.ToString(), mUrl));//reported to moderator (e.g. e-mail is sended)
            Assert.AreEqual(result, (int)CategoryCreateReturns.Success);
        }

        [TestMethod]
        public void Not_Create_Category_Twice()
        {
            //Arrange
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(m => m.Categories).Returns(new Category[] {new Category{Id = 0, Name = "exist category"}}.AsQueryable());
            Mock<IUrlManager> urlManagerMock = new Mock<IUrlManager>();
            string mUrl = "mock url";
            urlManagerMock.Setup(m => m.GetEditUrl(It.IsAny<Category>())).Returns(mUrl);
            Mock<IModerationProcessor> moderationProcessorMock = new Mock<IModerationProcessor>();

            BlogContentCreator target = new BlogContentCreator(urlManagerMock.Object,
                moderationProcessorMock.Object);
            var category = new Category
            {
                Id = 1,
                Name = "exist category"
            };

            //Act
            var result = target.CreateCategory(category, categoryRepositoryMock.Object);

            //Assert
            categoryRepositoryMock.Verify(m => m.SaveCategory(category), Times.Never);//our category is saved
            moderationProcessorMock.Verify(m => m.ProcessModeration(category.ToString(), mUrl), Times.Never);//didn't report to moderator (e.g. e-mail isn't sended)
            Assert.AreEqual(result, (int)CategoryCreateReturns.AlreadyExist);//system report that operation failed
        }
    }
}
