using CustomerManagement.Entities;
using Xunit;

namespace CustomerManagement.Test.NoteRepository
{

    public class NoteRepositoryTest
    {
        private readonly NoteRepositoryFixture Fixture = new NoteRepositoryFixture();

        [Fact]
        public void ShouldBeAbleToCreateNoteRepo()
        {
            Repositories.NoteRepository repository = Fixture.GetNoteRepository();
            Assert.NotNull(repository);
        }

        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            Fixture.DeleteAll();

            Repositories.NoteRepository repository = Fixture.GetNoteRepository();
            Note note = Fixture.GetNote();

            var createdNote = repository.Create(note);

            Assert.NotNull(createdNote);
        }

        [Fact]
        public void ShouldBeAbleToReadNote()
        {
            Fixture.DeleteAll();

            Repositories.NoteRepository repository = Fixture.GetNoteRepository();
            Note note = Fixture.GetNote();

            var createdNote = repository.Create(note);

            var readedNote = repository.Read(createdNote.Id);

            Assert.Equal(createdNote.Id, readedNote.Id);
            Assert.Equal(createdNote.CustomerId, readedNote.CustomerId);
            Assert.Equal(createdNote.Text, readedNote.Text);
        }

        [Fact]
        public void ShouldBeAbleToUpdateNote()
        {
            Fixture.DeleteAll();

            Repositories.NoteRepository repository = Fixture.GetNoteRepository();
            Note note = Fixture.GetNote();

            var createdNote = repository.Create(note);
            createdNote.Text = "New Text";

            repository.Update(createdNote);

            var updatedNote = repository.Read(createdNote.Id);

            Assert.NotEqual(note.Text, updatedNote.Text);
            Assert.Equal(createdNote.Text, updatedNote.Text);
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        {
            Fixture.DeleteAll();

            Repositories.NoteRepository repository = Fixture.GetNoteRepository();
            Note note = Fixture.GetNote();

            var createdNote = repository.Create(note);

            repository.Delete(createdNote.Id);

            var readedNote = repository.Read(createdNote.Id);

            Assert.Null(readedNote);
        }

    }
}
