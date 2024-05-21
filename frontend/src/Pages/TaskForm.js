import React, { useState } from 'react';
import './TaskForm.css'; // Import CSS file for TaskForm styling

const TaskForm = ({ onSubmit, initialValues }) => {
    const [title, setTitle] = useState(initialValues.title || '');
    const [description, setDescription] = useState(initialValues.description || '');
    const [dueDate, setDueDate] = useState(initialValues.dueDate || '');
    const [category, setCategory] = useState(initialValues.category || '');
    const [priority, setPriority] = useState(initialValues.priority || '');

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit({ title, description, dueDate, category, priority });
    };

    return (
        <form className="task-form" onSubmit={handleSubmit}>
            <h2>{initialValues ? 'Edit Task' : 'Add New Task'}</h2>
            <div className="form-group">
                <label>Title</label>
                <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} placeholder="Enter task title" />
            </div>
            <div className="form-group">
                <label>Description</label>
                <textarea value={description} onChange={(e) => setDescription(e.target.value)} placeholder="Enter task description" />
            </div>
            <div className="form-group">
                <label>Due Date</label>
                <input type="date" value={dueDate} onChange={(e) => setDueDate(e.target.value)} />
            </div>
            <div className="form-group">
                <label>Category</label>
                <input type="text" value={category} onChange={(e) => setCategory(e.target.value)} placeholder="Enter task category" />
            </div>
            <div className="form-group">
                <label>Priority</label>
                <input type="text" value={priority} onChange={(e) => setPriority(e.target.value)} placeholder="Enter task priority" />
            </div>
            <button type="submit">{initialValues ? 'Update Task' : 'Add Task'}</button>
        </form>
    );
};

export default TaskForm;
