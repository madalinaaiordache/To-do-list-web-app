import React, { useState, useContext, useEffect } from 'react';
import axios from 'axios';
import { AuthContext } from '../Context/AuthContext';
import './LoginPage.css'; // importăm fișierul CSS
import { useNavigate } from 'react-router-dom';
import Homepage from "../Components/Homepage";

function LoginPage() {
    const [form, setForm] = useState({ username: "", password: "" });
    const [message, setMessage] = useState(""); // adăugăm o nouă stare pentru mesaj
    const [isSuccessful, setIsSuccessful] = useState(false); // adăugăm o nouă stare pentru a urmări dacă autentificarea a avut succes sau nu
    const { isAuthenticated, setIsAuthenticated } = useContext(AuthContext); // adăugăm contextul de autentificare
    const navigate = useNavigate();

    const handleLoginSuccess = () => {
        setIsSuccessful(true);
        setIsAuthenticated(true);
        navigate('/');
    };

    useEffect(() => {
        if (isAuthenticated) {
            navigate('/');
        }
    }, [isAuthenticated, navigate]);

    const handleChange = e => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = e => {
        e.preventDefault();
        axios.post('https://localhost:7139/api/account/login', {
            username: form.username,
            password: form.password,
        })
        .then(response => {
            console.log(response);
            localStorage.setItem('token', response.data.token);
            setMessage("Autentificare reușită!");
            
            handleLoginSuccess();
        })
        .catch(error => {
            console.log(error);
            setMessage("A apărut o eroare la autentificare.");
            setIsSuccessful(false); // setăm starea ca fiind de eroare
            setIsAuthenticated(false); // actualizăm starea de autentificare
        });
    };

    return (
        <div className="login-container">
            {isAuthenticated ? (
                <Homepage />
            ) : (
                <div className="login-form">
                    <form onSubmit={handleSubmit}>
                        <input type="text" name="username" onChange={handleChange} placeholder="Username" required />
                        <input type="password" name="password" onChange={handleChange} placeholder="Password" required />
                        <button type="submit">Log in</button>
                    </form>
                    {message && <p className={`message ${isSuccessful ? 'success' : 'error'}`}>{message}</p>} {/* afișăm mesajul dacă există */}
                </div>
            )}
        </div>
    );
}

export default LoginPage;

