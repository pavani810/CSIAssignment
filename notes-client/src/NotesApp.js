import React, { useState, useEffect } from "react";

const NotesApp = () => {
  const [notes, setNotes] = useState([]);
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");

  // Load notes on mount
  useEffect(() => {
    const fetchNotes = async () => {
      try {
        const res = await fetch(`/api/notes`);
        const data = await res.json();
        setNotes(data);
      } catch (error) {
        console.error("Error fetching notes:", error);
      }
    };

    fetchNotes();
  }, []);

  // Add a new note
  const handleAddNote = async () => {
    const newNote = { title, content };

    try {
      const res = await fetch(`/api/notes`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newNote),
      });

      const note = await res.json();

      // prepend new note to state
      setNotes([note, ...notes]);
      setTitle("");
      setContent("");
    } catch (error) {
      console.error("Error adding note:", error);
    }
  };

  // Update a note
  const handleUpdateNote = async (id, updatedTitle, updatedContent) => {
    const updatedNote = { title: updatedTitle, content: updatedContent };

    try {
      const res = await fetch(`/api/notes/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(updatedNote),
      });

      const note = await res.json();

      // update state
      setNotes(notes.map((n) => (n.id === id ? note : n)));
    } catch (error) {
      console.error("Error updating note:", error);
    }
  };

  // Prompt the user for new title/content then call update
  const promptAndUpdate = (note) => {
    const newTitle = window.prompt("Edit title:", note.title);
    if (newTitle === null) return; // cancelled
    const newContent = window.prompt("Edit content:", note.content);
    if (newContent === null) return; // cancelled
    handleUpdateNote(note.id, newTitle, newContent);
  };

  // Delete a note
  const handleDeleteNote = async (id) => {
    try {
      await fetch(`/api/notes/${id}`, {
        method: "DELETE",
      });

      // remove from state
      setNotes(notes.filter((n) => n.id !== id));
    } catch (error) {
      console.error("Error deleting note:", error);
    }
  };

  return (
    <div style={{ padding: "20px" }}>
      <h1>Notes App</h1>

      <div style={{ marginBottom: "20px" }}>
        <input
          type="text"
          placeholder="Title"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          style={{ marginRight: "10px" }}
        />
        <input
          type="text"
          placeholder="Content"
          value={content}
          onChange={(e) => setContent(e.target.value)}
          style={{ marginRight: "10px" }}
        />
        <button onClick={handleAddNote}>Add Note</button>
      </div>

      <ul>
        {notes.map((note) => (
          <li key={note.id}>
            <strong>{note.title}</strong>: {note.content}
            <button
              style={{ marginLeft: "10px" }}
              onClick={() => promptAndUpdate(note)}
            >
              Update
            </button>
            <button
              style={{ marginLeft: "5px", color: "red" }}
              onClick={() => handleDeleteNote(note.id)}
            >
              Delete
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default NotesApp;
