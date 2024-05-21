import React from 'react';

const ConfirmationModal = ({ show, setShow, confirmAction, message }) => {
    const handleClose = () => setShow(false);
    const handleConfirm = () => {
        confirmAction();
        setShow(false);
    };

    return (
        <div className={show ? "modal display-block" : "modal display-none"}>
            <section className="modal-main">
                <p>{message}</p>
                <button onClick={handleConfirm}>Confirm</button>
                <button onClick={handleClose}>Cancel</button>
            </section>
        </div>
    );
};

export default ConfirmationModal;
