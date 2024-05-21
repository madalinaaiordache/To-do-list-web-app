import React, { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import './Navbar.css'; // Import your CSS file for styling
import { AuthContext } from '../Context/AuthContext';

const Navbar = () => {
  const { isAuthenticated, setIsAuthenticated } = useContext(AuthContext); // Correct usage of useContext
  const navigate = useNavigate(); // Use useNavigate hook for navigation

  const handleLogout = () => {
    // Clear authentication state and redirect to homepage
    setIsAuthenticated(false);
    localStorage.removeItem('token'); // Remove token from localStorage
    navigate('/'); // Navigate to homepage
  };

  return (
    <nav className="navbar">
      <div className="navbar-container">
        <Link to="/">Home</Link>
        <Link to="/aboutus">About Us</Link>
        <Link to="/faq">FAQ</Link>
        {isAuthenticated && <Link to="/mytasks">My Tasks</Link>} {/* Render My Tasks button if user is authenticated */}
        <div className="spacer"></div> {/* Add a spacer to create equal distance */}
        {!isAuthenticated ? ( // Check if user is not authenticated
          <>
            <Link to="/login">Login</Link>
            <Link to="/register">Register</Link>
          </>
        ) : (
          <button className="logout-button" onClick={handleLogout}>Logout</button> // Use a button for logout action
        )}
      </div>
    </nav>
  );
}

export default Navbar;
