import React, { createContext, useState } from 'react';

// Creating a context for authentication
export const AuthContext = createContext();

// Creating a provider component to wrap the entire application
export const AuthProvider = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  return (
    <AuthContext.Provider value={{ isAuthenticated, setIsAuthenticated }}>
      {children}
    </AuthContext.Provider>
  );
};
