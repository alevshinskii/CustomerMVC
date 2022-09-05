﻿using System;
using System.Web;
using CustomerManagement.Interfaces;
using System.Web.Mvc;
using CustomerWebMVC.Controllers;
using Moq;
using CustomerManagement.Entities;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebMVC.Test
{
    public class NoteControllerTest
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteController()
        {
            var controller = new NoteController();
            Assert.NotNull(controller);
        }

       [Fact]
        public void ShouldBeAbleToGetNotesList()
        {
            int customerId = 0;
            var notesList = new List<Note>() { new Note(), new Note() };

            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.ReadAll(customerId)).Returns(notesList);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Index(customerId);
            var view = result as ViewResult;
            Assert.NotNull(view);   

            noteRepoMock.Verify(x=>x.ReadAll(customerId));

            var model = view?.Model as List<Note>;
            Assert.Equal(notesList.Count,model?.Count);
        }

        [Fact]
        public void ShouldBeAbleToCallNoteCreatePage()
        { 
            int customerId = 0;

            var controller = new NoteController();
            var result = controller.Create(customerId);
            var view = result as ViewResult;

            Assert.NotNull(view);

            var model = view?.Model as Note;

            Assert.NotNull(model);

            Assert.Equal(customerId,model?.CustomerId);
        }

        
        [Fact]
        public void ShouldBeAbleToCallNoteEditPage()
        {
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Read(note.Id)).Returns(note);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Edit(note.Id);
            var view = result as ViewResult;

            Assert.NotNull(view);

            var model = view?.Model as Note;

            Assert.NotNull(model);
            Assert.Equal(note,model);
        }  
        
        [Fact]
        public void ShouldBeAbleToCallNoteDeletePage()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Read(note.Id)).Returns(note);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Delete(note.Id);
            var view = result as ViewResult;

            Assert.NotNull(view);

            var model = view?.Model as Note;

            Assert.NotNull(model);
            Assert.Equal(note,model);
        }

        [Fact]
        public void ShouldBeAbleToCreateNote()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Create(note)).Returns(note);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Create(note);

            noteRepoMock.Verify(x=>x.Create(note),Times.Once);

            var redirect = result as RedirectToRouteResult;

            Assert.NotNull(redirect);
            if (redirect != null) Assert.Contains("Index", redirect.RouteValues.Values);
        }

        [Fact]
        public void ShouldBeAbleToEditNote()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Update(note)).Returns(true);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Edit(note);

            noteRepoMock.Verify(x=>x.Update(note),Times.Once);

            var redirect = result as RedirectToRouteResult;

            Assert.NotNull(redirect);
            if (redirect != null) Assert.Contains("Index", redirect.RouteValues.Values);
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Delete(note.Id)).Returns(true);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Delete(note);

            noteRepoMock.Verify(x=>x.Delete(note.Id),Times.Once);

            var redirect = result as RedirectToRouteResult;

            Assert.NotNull(redirect);
            if (redirect != null) Assert.Contains("Index", redirect.RouteValues.Values);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateNoteIfRepoReturnsNull()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Create(note)).Returns(note=null);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Create(note);

            noteRepoMock.Verify(x=>x.Create(note),Times.Once);

            var view = result as ViewResult;
            Assert.NotNull(view);

            var model = view?.Model as Note;
            Assert.Equal(note,model);
        }

        [Fact]
        public void ShouldNotBeAbleToEditNoteIfRepoReturnsFalse()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Update(note)).Returns(false);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Edit(note);

            noteRepoMock.Verify(x=>x.Update(note),Times.Once);

            var view = result as ViewResult;
            Assert.NotNull(view);

            var model = view?.Model as Note;
            Assert.Equal(note,model);
        }

        [Fact]
        public void ShouldNotBeAbleToDeleteNoteIfRepoReturnsFalse()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Delete(note.Id)).Returns(false);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Delete(note);

            noteRepoMock.Verify(x=>x.Delete(note.Id),Times.Once);

            var view = result as ViewResult;
            Assert.NotNull(view);

            var model = view?.Model as Note;
            Assert.Equal(note,model);
        }

        [Fact]
        public void ShouldNotBeAbleToRedirectFromEditToNotFound()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            Note? noteNull = null;
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Read(note.Id)).Returns(noteNull);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Edit(note.Id);

            noteRepoMock.Verify(x=>x.Read(note.Id),Times.Once);

            var view = result as HttpNotFoundResult;
            Assert.NotNull(view);
        }

        [Fact]
        public void ShouldNotBeAbleToRedirectFromDeleteToNotFound()
        { 
            var note = new Note() { Id = 0, CustomerId = 0, Text = "text"};
            Note? noteNull = null;
            var noteRepoMock = new Mock<IRepository<Note>>();
            noteRepoMock.Setup(x => x.Read(note.Id)).Returns(noteNull);

            var controller = new NoteController(noteRepoMock.Object);
            var result = controller.Delete(note.Id);

            noteRepoMock.Verify(x=>x.Read(note.Id),Times.Once);

            var view = result as HttpNotFoundResult;
            Assert.NotNull(view);
        }






        [Fact]
        public void ShouldValidateNoteModel()
        {
            var note = new Note()
            {
                Text="new note"
            };
            var context = new ValidationContext(note, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(note, context, results, true);

            Assert.True(isModelStateValid);
        }

        [Fact]
        public void ShouldNotValidateInvalidNoteModel()
        {
            var note = new Note();
            var context = new ValidationContext(note, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(note, context, results, true);

            Assert.False(isModelStateValid);
        }

        [Fact]
        public void ShouldCreateReturnViewIfModelIsNotValid()
        {
            var controller = new NoteController();
            controller.ModelState.AddModelError("key", "message");

            var result = controller.Create(new Note());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShouldCreateReturnMessageIfModelIsNotValid()
        {
            var controller = new NoteController();
            controller.ModelState.AddModelError("key", "message");

            controller.Create(new Note());

            Assert.Equal("Note is not valid", controller.ViewBag.Message);
        }

        [Fact]
        public void ShouldEditReturnViewIfModelIsNotValid()
        {
            var controller = new NoteController();
            controller.ModelState.AddModelError("key", "message");

            var result = controller.Edit(new Note());

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ShouldEditReturnMessageIfModelIsNotValid()
        {
            var controller = new NoteController();
            controller.ModelState.AddModelError("key", "message");

            controller.Edit(new Note());

            Assert.Equal("Note is not valid", controller.ViewBag.Message);
        }
    }
}
