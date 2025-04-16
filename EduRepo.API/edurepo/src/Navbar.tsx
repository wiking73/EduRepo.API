import React, { useState } from 'react';
import './Styles/Login.css';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';
import { useNavigate } from "react-router-dom";

interface JwtPayload {
    unique_name: string;   
    nameid: string;        
    email: string;        
    role: string;      
    exp: number;        
}


interface LoginResponse {
    token: string;
    username: string;

}

interface RegisterResponse {
    token: string;
    username: string;
}

const Logowanie: React.FC = () => {
    const [showPassword, setShowPassword] = useState(false);
    const [loginInfo, setLoginInfo] = useState({
        email: '',
        password: ''
    });
    const [registerInfo, setRegisterInfo] = useState({
        username: '',
        phoneNumber: '',
        email: '',
        password: ''
    });
    const [error, setErrorMessage] = useState<string | null>(null);
    const [success, setSuccessMessage] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);
    const [isLogin, setIsLogin] = useState(true);

    const navigate = useNavigate();

    const togglePassword = () => {
        setShowPassword(!showPassword);
    };

    const handleLoginInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setLoginInfo((prev) => ({
            ...prev,
            [name]: value
        }));
    };

    const handleRegisterInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setRegisterInfo((prev) => ({
            ...prev,
            [name]: value
        }));
    };

    const handleLogin = async (e: React.FormEvent) => {
        e.preventDefault();
        setErrorMessage(null);
        try {
            setLoading(true);
            const response = await axios.post<LoginResponse>(
                'https://localhost:7157/api/Auth/login',
                loginInfo
            );

            if (response.status === 200) {
                const { token, username } = response.data;
                setSuccessMessage('Zalogowano pomyœlnie.');
                console.log('Zalogowano:', { token, username });
               
                
                localStorage.setItem('authToken', token);
                localStorage.setItem('displayName', username);


                const decoded = jwtDecode<JwtPayload>(token);
                localStorage.setItem('role', decoded.role);
                navigate("/dashboard")

            }
        } catch (error: any) {
            console.error('B³¹d logowania:', error);
            setErrorMessage(
                error.response?.data?.message ||
                'Próba logowania nie powiod³a siê. SprawdŸ swoje dane.'
            );
        } finally {
            setLoading(false);
        }
    };

    const handleRegister = async (e: React.FormEvent) => {
        e.preventDefault();
        setErrorMessage(null);
        try {
            setLoading(true);
            const response = await axios.post<RegisterResponse>(
                'https://localhost:7157/api/Auth/register',
                registerInfo
            );

            if (response.status === 200) {
                const { token, username } = response.data;
                setSuccessMessage('Rejestracja zakoñczona sukcesem.');
                console.log('Zarejestrowano:', { token, username });

                localStorage.setItem('authToken', token);
                localStorage.setItem('username', username);
                const decoded = jwtDecode<JwtPayload>(token);
                localStorage.setItem('role', decoded.role);


            }
        } catch (error: any) {
            console.error('B³¹d rejestracji:', error);
            setErrorMessage(
                error.response?.data?.message ||
                'Rejestracja nie powiod³a siê. Spróbuj ponownie.'
            );
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="login-page">
           
            <form
                onSubmit={isLogin ? handleLogin : handleRegister}
                className="login-form"
            >
             <p className="login-header">{isLogin ? 'Logowanie' : 'Rejestracja'}</p>
                <div className="button-container">
                    <button
                        type="button"
                        className="login-register-button1"
                        onClick={(e) => {
                            e.preventDefault();
                            setIsLogin(true);
                            setSuccessMessage(null);
                            setErrorMessage(null);
                        }}
                    >
                        Zaloguj siê
                    </button>
                    <button
                        type="button"
                        className="login-register-button2"
                        onClick={(e) => {
                            e.preventDefault();
                            setIsLogin(false);
                            setSuccessMessage(null);
                            setErrorMessage(null);
                        }}
                    >
                        Zarejestruj siê
                    </button>
                </div>

                {/* Pola w zale¿noœci od trybu: login vs register */}
                {isLogin ? (
                    <input
                        type="email"
                        id="email"
                        name="email"
                        className="email"
                        placeholder="Email"
                        value={loginInfo.email}
                        onChange={handleLoginInputChange}
                    />
                ) : (
                    <>
                        <input
                            type="text"
                            id="username"
                            name="username"
                            className="username"
                            placeholder="Nazwa u¿ytkownika"
                            value={registerInfo.username}
                            onChange={handleRegisterInputChange}
                        />
                        <input
                            type="email"
                            id="email"
                            name="email"
                            className="email"
                            placeholder="Email"
                            value={registerInfo.email}
                            onChange={handleRegisterInputChange}
                        />
                        
                    </>
                )}

                <input
                    type={showPassword ? 'text' : 'password'}
                    id="password"
                    className="haslo"
                    name="password"
                    placeholder="Has³o"
                    value={isLogin ? loginInfo.password : registerInfo.password}
                    onChange={isLogin ? handleLoginInputChange : handleRegisterInputChange}
                />

                <label className="pokaz">
                    <input
                        type="checkbox"
                        checked={showPassword}
                        onChange={togglePassword}
                    />
                    Poka¿ has³o
                </label>

                <input
                    type="submit"
                    className="zapisz"
                    value={isLogin ? 'Zaloguj siê' : 'Zarejestruj'}
                    disabled={loading}
                />

                {/* Komunikaty o b³êdzie / sukcesie */}
                {error && <p style={{ color: 'red' }}>{error}</p>}
                {success && <p style={{ color: 'green' }}>{success}</p>}
            </form>
        </div>
    );
};

export default Logowanie;