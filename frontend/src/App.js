import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Navbar from "./Components/Navbar";
import Footer from "./Components/Footer";
import Homepage from "./Components/Homepage";
import LoginPage from "./Pages/LoginPage";
import RegisterPage from "./Pages/RegisterPage";
import Aboutus from "./Pages/Aboutus";
import ConfirmationModal from "./Pages/ConfirmationModal";
import Faq from "./Pages/Faq";
import MyTasks from "./Pages/MyTasks"; 
import { AuthProvider, AuthContext } from './Context/AuthContext';
import { Navigate } from 'react-router-dom';


function App() {
    return (
        <AuthProvider>
            <Router>
                <div className="App">
                    <Navbar />
                    <div className="App-content">
                        <AppRoutes />
                    </div>
                    <Footer />
                </div>
            </Router>
        </AuthProvider>
    );
}

function AppRoutes() {
    const { isAuthenticated } = React.useContext(AuthContext);
    return (
        <Routes>
            <Route path="/" element={<Homepage />} />
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/aboutus" element={<Aboutus />} />
            <Route path="/faq" element={<Faq />} />
            {isAuthenticated ? (
                <Route path="/mytasks" element={<MyTasks />} />
            ) : (
                <Route path="/mytasks" element={<Navigate to="/" />} />
            )}        </Routes>
    );
}

export default App;
