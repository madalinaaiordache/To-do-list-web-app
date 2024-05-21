import React, { useState } from 'react';
import axios from 'axios';
import './RegisterPage.css'; // Import your CSS file

function RegisterPage() {
    const [form, setForm] = useState({ username: "", email: "", password: "", confirmPassword: "", role: "User" });
    const [message, setMessage] = useState("");
    const [isSuccessful, setIsSuccessful] = useState(false);

    const handleChange = e => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = e => {
        e.preventDefault();
        if (form.password === form.confirmPassword) {
            axios.post('https://localhost:7139/api/account/register', {
                username: form.username,
                email: form.email,
                password: form.password,
                role: form.role
            })
            .then(response => {
                console.log(response);
                setMessage("Account registered successfully!");
                setIsSuccessful(true);
            })
            .catch(error => {
                console.log(error);
                setMessage("An error occurred during registration.");
                setIsSuccessful(false);
            });
        } else {
            setMessage('Passwords do not match!');
        }
    };

    return (
        <div className="register-container">
            <div className="register-form">
                <form onSubmit={handleSubmit}>
                    <input type="text" name="username" onChange={handleChange} placeholder="Username" required />
                    <input type="email" name="email" onChange={handleChange} placeholder="Email" required />
                    <input type="password" name="password" onChange={handleChange} placeholder="Password" required />
                    <input type="password" name="confirmPassword" onChange={handleChange} placeholder="Confirm password" required />
                    <select name="role" onChange={handleChange}>
                        <option value="User">User</option>
                        <option value="Admin">Admin</option>
                    </select>
                    <button type="submit">Register</button>
                </form>
                {message && <p className={`message ${isSuccessful ? 'success' : 'error'}`}>{message}</p>}
            </div>
        </div>
    );
}

export default RegisterPage;
