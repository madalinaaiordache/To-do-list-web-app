import React from 'react';
import './Homepage.css'; // Create and import a CSS file for the homepage styles

const Homepage = () => {
    return (
        <div className="homepage-container">
            <h1 className="main-heading">TO DO LISTER</h1>
            <h1 className="main-heading">A simple to do list to manage it all</h1>
            <p className="sub-heading">Easily manage your personal tasks, family projects, and team's work all in one place.</p>
            <button className="cta-button">Get Started. It's FREE</button>
            <p className="cta-subtext">FREE FOREVER. NO CREDIT CARD.</p>
        </div>
    );
}

export default Homepage;
