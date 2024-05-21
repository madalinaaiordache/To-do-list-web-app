import React from 'react';
import './Aboutus.css';
import image5 from '../images/image5.jpg'; // Import your images for the page
import image3 from '../images/image3.jpg';
import image4 from '../images/image4.jpg';

const Aboutus = () => {
  return (
    <div className="about-us-container">
      <div className="section">
        <h1 className="title">Todolister</h1>
        <p className="description">
          Versatile and intuitive to-do list web application designed to help users efficiently manage their tasks and stay organized. Whether you're a busy professional, a student with assignments, or someone looking to streamline your daily activities, Todolister provides a seamless and user-friendly interface to enhance your productivity.
        </p>
      </div>
      <div className="section2">
        <div className="content">
          <h2 className="sub-title">Organize your to-do lists from anywhere.</h2>
          <p className="subtitle">Create clear, multi-functional to-do lists to easily manage your ideas and work from anywhere so you never forget anything again.</p>
        </div>
        <img src={image5} alt="Organize your to-do lists" className="image" />
      </div>
      <div className="section3">
        <div className="content">
          <h2 className="sub-title">Manage your to-do's from anywhere.</h2>
          <p className="subtitle">Create and access your to-do lists from anywhere. Now you'll never miss an idea or forget what you need to do next.</p>
        </div>
        <img src={image3} alt="Manage your to-do's from anywhere" className="image" />
      </div>
      <div className="section2">
        <div className="content">
          <h2 className="sub-title">Never miss a task or idea again.</h2>
          <p className="subtitle">Home view makes it easy to view and customize everything you need to work on. Set reminders, reschedule tasks, and assign priorities so you never lose anything again.</p>
        </div>
        <img src={image4} alt="Never miss a task or idea again." className="image" />
      </div>
    </div>
  );
}

export default Aboutus;
