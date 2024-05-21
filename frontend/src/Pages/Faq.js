import React, { useState } from 'react';

import './Faq.css';

const Faq = () => {
  const [searchTerm, setSearchTerm] = useState('');
  const [activeIndex, setActiveIndex] = useState(null);
  const [showDetails, setShowDetails] = useState(false);
  const [detailsContent, setDetailsContent] = useState('');

  const faqData = [
    { question: 'Can I access my to-do lists from different devices?', answer: 'Yes, Todolister allows you to access your to-do lists from any device with an internet connection.' },
    { question: 'Can I attach files or notes to a task?', answer: 'Currently, Todolister doesn’t support attachments, but you can add notes directly in the task description.' },
    { question: 'Can I organize my tasks into different categories or projects?', answer: 'Yes! You can create custom categories or projects to organize your tasks. Click on “Add List” and give your new list a name.' },
    { question: 'Can I set custom notifications for tasks?', answer: 'Todolister doesn’t have built-in notifications, but you can use external tools like calendar apps or reminders.' },
    { question: 'Can I set recurring tasks?', answer: 'Unfortunately, Todolister doesn’t have built-in recurring tasks, but you can manually duplicate tasks with the same due date.' },
    { question: 'Can I sort tasks by creation date or modification date?', answer: 'Currently, Todolister only supports manual sorting. You can organize tasks as needed.' },
    { question: 'How can I change the due date for a task?', answer: 'Click on the task, and you’ll see an option to edit the due date. Update it according to your preference.' },
    { question: 'How can I create a new to-do list?', answer: 'You can create a new to-do list by clicking on the "Add List" button on the homepage and entering the necessary details.' },
    { question: 'How can I set reminders for my tasks?', answer: 'You can set reminders for your tasks by clicking on the task and selecting the "Set Reminder" option.' },
    { question: 'How do I mark a task as completed?', answer: 'To mark a task as completed, simply click on the checkbox next to the task. It will be crossed out to indicate completion.' },
    { question: 'How do I change the order of tasks within a list?', answer: 'Simply drag and drop tasks to rearrange their order. The new order will be saved automatically.' },
    { question: 'Is there a way to share my to-do lists with others?', answer: 'Currently, Todolister doesn’t support direct sharing, but you can export your lists as text or CSV files and share them manually.' },
    { question: 'Is there a way to filter tasks by priority?', answer: 'Yes! Click on the priority level (e.g., high, medium, low) to filter tasks based on their importance.' },
    { question: 'What happens if I accidentally close the browser without saving my changes?', answer: 'Todolister automatically saves your changes periodically. If you close the browser accidentally, your data should still be intact.' },
    { question: 'What happens if I accidentally delete a task?', answer: 'Don’t worry! Deleted tasks are moved to the trash. You can restore them from there or permanently delete them.' },
    { question: 'What is Todolister?', answer: 'Todolister is a versatile and intuitive to-do list web application designed to help users efficiently manage their tasks and stay organized.' },
    { question: 'What’s the maximum number of tasks I can have in a single list?', answer: 'There’s no strict limit, but keep your lists manageable for better organization and performance.' },
    { question: 'How secure is my data in Todolister?', answer: 'Todolister encrypts your data and stores it securely. However, always follow best practices like using strong passwords and avoiding public computers.' },
  ];

  // Sort the FAQ data alphabetically by question
  const sortedFaqData = faqData.sort((a, b) => a.question.localeCompare(b.question));

  const handleToggle = (index) => {
    setActiveIndex(activeIndex === index ? null : index);
  };

  const toggleDetails = (content) => {
    setDetailsContent(content);
    setShowDetails(!showDetails);
  };

  const filteredFaqs = sortedFaqData.filter(faq =>
    faq.question.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div className="faq-container">
      <h1 className="faq-title">Frequently Asked Questions</h1>
      <div className="search-bar">
        <input
          type="text"
          placeholder="Search..."
          className="search-input"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />
      </div>
      {filteredFaqs.map((item, index) => (
        <div key={index} className={`faq-item ${activeIndex === index ? 'active' : ''}`}>
          <div className="faq-question" onClick={() => handleToggle(index)}>
            {item.question}
            <span className="faq-icon">{activeIndex === index ? '▲' : '▼'}</span>
          </div>
          <div className="faq-answer">{item.answer}</div>
        </div>
      ))}

    <div className="about-us-containerT">
      <div className="sectionT">
        <h1 className="titleT">Todolister Application Features</h1>
      </div>
      
      <div className="section2T">
        <div className="contentT">
          <h2 className="sub-titleT">Organize your to-do lists from anywhere.</h2>
          <p className="subtitleT">This table presents the key features of the Todolister application, a task management tool designed to help users organize their tasks effectively. Each row in the table corresponds to a specific feature of the application, providing a concise description of its functionality, availability, and an option to view additional details.</p>
        </div>
      </div>
    </div>


     {/* Table for Todolister Application Features */}
     <div className="faq-table">
    <h2>Todolister Application Features</h2>
    <table>
        <thead>
        <tr>
            <th className="feature-header">Feature</th>
            <th className="description-header">Description</th>
            <th className="availability-header">Availability</th>
            <th className="details-header">Details</th>
        </tr>
        </thead>
        <tbody>
        <tr>
            <td>Create task list</td>
            <td>Allows the user to create new task lists</td>
            <td>Available</td>
            {/* <td><button onClick={() => toggleDetails('....')}>Details</button></td> */}
            <td><button onClick={() => toggleDetails('The "Create task list" feature empowers users to establish personalized lists tailored to their unique requirements, thereby facilitating efficient organization and management of tasks across various contexts. With this functionality, users can delineate distinct categories or projects, each representing a cohesive set of tasks aligned with specific objectives or responsibilities. By offering the flexibility to customize list names and structures, this feature promotes a granular approach to task management, accommodating diverse workflows and preferences. Moreover, it fosters clarity and coherence by enabling users to compartmentalize their tasks based on criteria such as priority, timeline, or thematic relevance. Whether for professional endeavors, academic pursuits, or personal aspirations, the ability to create task lists serves as a cornerstone for users to structure their workload effectively, prioritize activities, and ultimately enhance productivity and fulfillment in their endeavors.')}>Details</button></td>
        </tr>
        <tr>
            <td>Add tasks to list</td>
            <td>User can add new tasks to existing lists</td>
            <td>Available</td>
            <td><button onClick={() => toggleDetails('The "Add tasks to list" feature empowers users with the capability to seamlessly augment their existing task lists with new items, thereby facilitating dynamic task management and continual workflow enhancement. With this functionality, users enjoy the flexibility to swiftly capture new tasks as they arise, ensuring that no important assignment or commitment goes overlooked. Whether it is jotting down a sudden inspiration, noting an upcoming deadline, or logging a recurring responsibility, users can effortlessly expand their task lists to accommodate a diverse array of tasks and commitments. By streamlining the task creation process and integrating it seamlessly into their existing workflow, this feature promotes productivity and helps users stay organized and on top of their responsibilities. Additionally, it fosters a sense of empowerment and control, enabling users to actively engage with their tasks and curate their lists in real-time, thereby enhancing their ability to prioritize effectively and achieve their goals with confidence and efficiency.')}>Details</button></td>
        </tr>
        <tr>
            <td>Edit tasks</td>
            <td>User can edit details of an existing task</td>
            <td>Available</td>
            <td><button onClick={() => toggleDetails('The "Edit tasks" feature offers users the flexibility and precision to fine-tune the details of their existing tasks, empowering them to tailor their to-do lists to their evolving needs and preferences with ease. This functionality goes beyond mere task creation and deletion, allowing users to delve deeper into their task management process by modifying specific attributes of individual tasks. Whether it is adjusting due dates, refining task descriptions, reassigning priorities, or updating task statuses, users have the freedom to make nuanced changes that reflect the dynamic nature of their tasks and commitments. By enabling users to edit task details directly within the application interface, this feature promotes efficiency and streamlines the task management workflow, eliminating the need for cumbersome workarounds or external tools. Moreover, it fosters a sense of ownership and agency, empowering users to take control of their task lists and customize them according to their unique preferences and priorities. With the ability to edit tasks seamlessly integrated into their task management toolkit, users can navigate complex schedules and evolving priorities with confidence, ensuring that their to-do lists remain accurate, relevant, and aligned with their goals.')}>Details</button></td>
        </tr>
        <tr>
            <td>Delete tasks</td>
            <td>User can delete tasks from existing lists</td>
            <td>Available</td>
            <td><button onClick={() => toggleDetails('The "Delete tasks" feature is a crucial aspect of task management, offering users the capability to remove unnecessary or completed tasks from their existing lists with ease and efficiency. Beyond mere task creation and completion, the ability to delete tasks provides users with a sense of control and organization over their to-do lists. This feature empowers users to streamline their task management process by decluttering their lists and focusing on the tasks that truly matter. By eliminating irrelevant or completed tasks, users can maintain clarity and focus, ensuring that their to-do lists remain concise and actionable. Furthermore, the option to delete tasks promotes flexibility and adaptability, allowing users to adjust their task lists in response to changing priorities or evolving circumstances. Whether it is removing outdated tasks, clearing completed items, or simply tidying up their lists for better organization, the "Delete tasks" feature enables users to maintain a clean and efficient task management environment. With the ability to delete tasks readily available within the application interface, users can navigate their to-do lists confidently, knowing that they have the tools to manage their tasks effectively and keep their workload under control.')}>Details</button></td>
        </tr>
        <tr>
            <td>Set priorities</td>
            <td>User can assign priorities to tasks (e.g., high, medium, low)</td>
            <td>Available</td>
            <td><button onClick={() => toggleDetails('The "Set priorities" feature is a fundamental aspect of task management that allows users to categorize and organize their tasks based on their relative importance or urgency. By assigning priorities such as high, medium, or low to individual tasks, users can effectively prioritize their workload and focus their attention on the most critical tasks first. This feature enables users to differentiate between tasks that require immediate action and those that can be addressed at a later time, helping them allocate their time and resources more efficiently. Additionally, by visually categorizing tasks based on priority levels, users gain a clear understanding of their task list is overall significance and can make informed decisions about where to direct their efforts. Whether it is meeting tight deadlines, tackling important projects, or simply staying on top of daily responsibilities, the ability to set priorities empowers users to manage their tasks proactively and stay productive amidst various demands and deadlines. Moreover, the flexibility of defining custom priority levels allows users to adapt the prioritization system to their specific needs and preferences, ensuring that they can tailor their task management approach to suit their unique workflow and objectives. Ultimately, the "Set priorities" feature enhances users ability to organize and prioritize their tasks effectively, enabling them to stay focused, productive, and in control of their workload.')}>Details</button></td>
        </tr>
        <tr>
            <td>Set deadlines</td>
            <td>User can set deadlines for tasks</td>
            <td>Available</td>
            <td><button onClick={() => toggleDetails('The "Set deadlines" feature is a crucial aspect of task management that empowers users to establish clear timelines and deadlines for completing their tasks. By defining specific due dates or deadlines for each task, users can effectively plan and organize their workflow, ensuring that important tasks are completed on time and preventing procrastination or missed deadlines. This feature enables users to prioritize their tasks based on urgency, allowing them to allocate their time and resources efficiently and focus on completing tasks that are due sooner rather than later. Additionally, setting deadlines provides users with a sense of accountability and helps them stay motivated and disciplined in their task management approach. By having clear deadlines in place, users are better able to track their progress, monitor upcoming deadlines, and take timely action to meet their obligations. Whether it is completing work projects, meeting project milestones, or simply managing daily errands and responsibilities, the ability to set deadlines allows users to stay organized, stay on track, and ultimately achieve their goals more effectively. Moreover, this feature often includes features like reminders or notifications to alert users of upcoming deadlines, further enhancing their ability to manage their time and workload efficiently.')}>Details</button></td>
        </tr>
        {/* Add more features here */}
        </tbody>
    </table>
    </div>

    {/* Details Box */}
    {showDetails && (
        <div className="details-box">
          <button className="close-button" onClick={() => setShowDetails(false)}>X</button>
          <p>{detailsContent}</p>
        </div>
      )}
    </div>
  );
};

export default Faq;
