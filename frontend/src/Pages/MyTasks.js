import React, { useState, useEffect } from 'react';
import './MyTasks.css';
import axios from 'axios';
import Modal from 'react-modal';

Modal.setAppElement('#root'); // Set the root element for accessibility

const MyTasks = () => {
    const [tasks, setTasks] = useState([]);
    const [task, setTask] = useState({
        title: '',
        description: '',
        dueDate: '',
        categoryID: '',
        priorityID: ''
    });
    const [categories, setCategories] = useState([]);
    const [priorities, setPriorities] = useState([]);
    const [filter, setFilter] = useState({
        categoryID: '',
        priorityID: ''
    });
    const [search, setSearch] = useState('');
    const [modalIsOpen, setModalIsOpen] = useState(false);
    const [taskToDelete, setTaskToDelete] = useState(null);

    const [currentPage, setCurrentPage] = useState(1); // Current page state
    const [tasksPerPage] = useState(5); // Tasks per page state

    useEffect(() => {
        fetchTasks();
        fetchCategories();
        fetchPriorities();
    }, []);

    const fetchTasks = async () => {
        try {
            const token = localStorage.getItem('token');
            const config = {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            };
            const response = await axios.get('https://localhost:7139/api/Task', config);
            // setTasks(response.data.$values);
            const tasksWithFormattedDates = response.data.$values.map(task => ({
                ...task,
                dueDate: task.dueDate.split('T')[0] // Format the due date
            }));
            setTasks(tasksWithFormattedDates);
        } catch (error) {
            console.error('Error fetching tasks:', error);
        }
    };

    const fetchCategories = async () => {
        try {
            const token = localStorage.getItem('token');
            const config = {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            };
            const response = await axios.get('https://localhost:7139/api/Category', config);
            setCategories(response.data.$values);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const fetchPriorities = async () => {
        try {
            const token = localStorage.getItem('token');
            const config = {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            };
            const response = await axios.get('https://localhost:7139/api/Priority', config);
            setPriorities(response.data.$values);
        } catch (error) {
            console.error('Error fetching priorities:', error);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setTask(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleFilterChange = (e) => {
        const { name, value } = e.target;
        setFilter(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSearchChange = (e) => {
        setSearch(e.target.value);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const token = localStorage.getItem('token');
            const config = {
                headers: {
                    'Authorization': `Bearer ${token}`,
                    'Content-Type': 'application/json'
                }
            };
    
            const formattedDueDate = new Date(task.dueDate).toISOString();
            const taskToSend = { ...task, dueDate: formattedDueDate };
    
            if (task.taskID) {
                const url = `https://localhost:7139/api/Task/${task.taskID}`;
                const updatedTask = { ...task, dueDate: formattedDueDate };
                await axios.put(url, updatedTask, config);
                setTasks(tasks.map(t => t.taskID === task.taskID ? { ...updatedTask, dueDate: updatedTask.dueDate.split('T')[0] } : t));
            } else {
                const url = 'https://localhost:7139/api/Task';
                const response = await axios.post(url, taskToSend, config);
                if (response.data) {
                    const newTask = response.data.$values[0];
                    setTasks([...tasks, { ...newTask, dueDate: newTask.dueDate.split('T')[0] }]);
                }
            }
    
            setTask({
                title: '',
                description: '',
                dueDate: '',
                categoryID: '',
                priorityID: ''
            });
        } catch (error) {
            console.error('Error handling task:', error);
        }
    };
    

    const handleEdit = (task) => {
        const taskWithFormattedDate = {
            ...task,
            dueDate: task.dueDate.split('T')[0] // Format the due date
        };
        setTask(taskWithFormattedDate);
    };

    const handleDelete = async () => {
        try {
            const token = localStorage.getItem('token');
            const config = {
                headers: {
                    'Authorization': `Bearer ${token}`,
                }
            };

            const url = `https://localhost:7139/api/Task/${taskToDelete.taskID}`;
            const response = await axios.delete(url, config);
            setTasks(tasks.filter(t => t.taskID !== taskToDelete.taskID));
            setModalIsOpen(false);
        } catch (error) {
            console.error('Error deleting task:', error);
        }
    };

    const openModal = (task) => {
        setTaskToDelete(task);
        setModalIsOpen(true);
    };

    const closeModal = () => {
        setModalIsOpen(false);
        setTaskToDelete(null);
    };

    // Calculate tasks for the current page
    const indexOfLastTask = currentPage * tasksPerPage;
    const indexOfFirstTask = indexOfLastTask - tasksPerPage;
    const currentTasks = tasks.slice(indexOfFirstTask, indexOfLastTask);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const filteredTasks = currentTasks.filter(t =>
        (filter.categoryID ? t.categoryID === filter.categoryID : true) &&
        (filter.priorityID ? t.priorityID === filter.priorityID : true) &&
        (search ? t.title.toLowerCase().includes(search.toLowerCase()) || t.description.toLowerCase().includes(search.toLowerCase()) : true)
    );

    return (
        <div className="homepage-containerTT">
            <h1 className="main-headingTT">My Tasks</h1>

            <form onSubmit={handleSubmit} className="task-form">
                <input type="text" name="title" value={task.title} onChange={handleChange} placeholder="Task Title" required />
                <input type="text" name="description" value={task.description} onChange={handleChange} placeholder="Description" required />
                <input type="date" name="dueDate" value={task.dueDate} onChange={handleChange} required />

                <select name="categoryID" value={task.categoryID} onChange={handleChange} required>
                    <option value="">Select Category</option>
                    {categories.map((category, index) => (
                        <option key={index} value={category.categoryID}>{category.name}</option>
                    ))}
                </select>

                <select name="priorityID" value={task.priorityID} onChange={handleChange} required>
                    <option value="">Select Priority</option>
                    {priorities.map((priority, index) => (
                        <option key={index} value={priority.priorityID}>{priority.description}</option>
                    ))}
                </select>
                <button type="submit">Add Task</button>
            </form>

            <div className="filters">
                <input
                    type="text"
                    placeholder="Search tasks..."
                    value={search}
                    onChange={handleSearchChange}
                />
                <select name="categoryID" value={filter.categoryID} onChange={handleFilterChange}>
                    <option value="">Filter by Category</option>
                    {categories.map((category, index) => (
                        <option key={index} value={category.categoryID}>{category.name}</option>
                    ))}
                </select>

                <select name="priorityID" value={filter.priorityID} onChange={handleFilterChange}>
                    <option value="">Filter by Priority</option>
                    {priorities.map((priority, index) => (
                        <option key={index} value={priority.priorityID}>{priority.description}</option>
                    ))}
                </select>
            </div>

            <table className="task-table">
                <thead>
                    <tr>
                        <th>Task Title</th>
                        <th>Description</th>
                        <th>Due Date</th>
                        <th>Category</th>
                        <th>Priority</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {Array.isArray(filteredTasks) && filteredTasks.map((task, index) => (
    <tr key={index}>
        <td>{task.title}</td>
        <td>{task.description}</td>
        <td>{task.dueDate}</td>
        <td>{categories.find(cat => cat.categoryID === task.categoryID)?.name}</td>
        <td>{priorities.find(pri => pri.priorityID === task.priorityID)?.description}</td>
        <td>
            <button onClick={() => handleEdit(task)}>Edit</button>
            <button onClick={() => openModal(task)}>Delete</button>
        </td>
    </tr>
))}
</tbody>
</table>

{/* Pagination */}
<div className="pagination">
    <button onClick={() => paginate(currentPage - 1)} disabled={currentPage === 1}>Previous</button>
    {tasksPerPage * currentPage < tasks.length && (
        <button onClick={() => paginate(currentPage + 1)}>Next</button>
    )}
</div>

<Modal
    isOpen={modalIsOpen}
    onRequestClose={closeModal}
    contentLabel="Confirm Delete"
    className="modal"
    overlayClassName="modal-overlay"
>
    <h2>Confirm Delete</h2>
    <p>Are you sure you want to delete this task?</p>
    <div className="modal-buttons">
        <button onClick={handleDelete}>Yes</button>
        <button onClick={closeModal}>No</button>
    </div>
</Modal>
</div>
);
};

export default MyTasks;


