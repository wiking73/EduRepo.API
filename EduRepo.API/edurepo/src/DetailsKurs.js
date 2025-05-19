import React, { useEffect, useState } from 'react';
import { useParams, Link, NavLink, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Styles/Details.css';
import { Menu, Button, List } from 'semantic-ui-react';


function KursDetails() {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [kurs, setKurs] = useState(null);
    const [zadania, setZadania] = useState([]);
    const role = localStorage.getItem("role");
    const name = localStorage.getItem("username");


    const { id } = useParams();
    const navigate = useNavigate();

    const fetchKurs = async () => {
        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${id}`);
            setKurs(response.data);
        } catch (err) {
            console.error('Błąd podczas pobierania kursu:', err);
            setError('Nie udało się pobrać danych kursu.');
        } finally {
            setIsLoading(false);
        }
    };

    const fetchZadania = async () => {
        try {
            const response = await axios.get(`https://localhost:7157/api/Zadanie`);
            setZadania(response.data);
        } catch (err) {
            console.error('Błąd podczas pobierania zadań:', err);
            setError('Nie udało się pobrać zadaż=ń.');
        }
    };

    useEffect(() => {
        fetchKurs();
        fetchZadania();
    }, [id]);

    if (isLoading) {
        return <p>ładowanie danych kursu...</p>;
    }

    if (error) {
        return <p style={{ color: 'red' }}>{error}</p>;
    }

    if (!kurs) {
        return <p>Nie znaleziono kursu</p>;
    }

    const handleAddZadanie = () => {
        console.log('Dodaj zadanie dla kursu (tylko Admin)');
    };
    const handleAddOdpowiedz = () => {
        console.log('Dodaj odpowiedź do zadania (tylko Admin)');
    };
    const handleOdpowiedz = () => {
        console.log('Zobacz odpowiedzi');
    };

    const zadaniaDlaKursu = zadania.filter((zadanie) => zadanie.idKursu === parseInt(id, 10));

    return (
        (kurs.userName === name || role === "Admin" ) ? (
            <div className="kurs-details">
                <h4>{kurs.nazwa}</h4>
                <h3>Szczegółowe Informacje</h3>
                <p><strong>Opis:</strong> {kurs.opisKursu}</p>
                <p><strong>Klasa:</strong> {kurs.klasa}</p>
                <p><strong>Rok akademicki:</strong> {kurs.rokAkademicki}</p>
                <Link to="/kursy" className="btn btn-secondary">
                    Powrót do kursów
                </Link>

                {role === "Teacher" && (
                    <div className="add-task-button">
                        <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/create`}>
                            <Button content="Dodaj Zadanie" size="large" className="custom-button17" onClick={handleAddZadanie} />
                        </Menu.Item>
                        <Menu.Item as={NavLink} to={`/kurs/${id}/uczestnicy`}>
                            <Button content="Uczestnicy" size="large" className="custom-button17" onClick={handleAddZadanie} />
                        </Menu.Item>
                    </div>
                )}

                <p><strong>Stworzony przez:</strong> {kurs.userName}</p>

                <h3>Lista zadań</h3>
                {zadaniaDlaKursu.length > 0 ? (
                    <List>
                        {zadaniaDlaKursu.map((zadanie) => (
                            <List.Item key={zadanie.idZadania}>
                                <List.Content>
                                    <List.Header>{zadanie.nazwa}</List.Header>
                                    <p><strong>Termin oddania:</strong> {new Date(zadanie.terminOddania).toLocaleString()}</p>
                                    <p>{zadanie.tresc}</p>
                                    <Link to={`/zadanie/${zadanie.idZadania}`} className="btn btn-primary">
                                        Szczegóły zadania
                                    </Link>
                                    {role === "Student" && (
                                        <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedz`}>
                                            <Button content="Dodaj Odpowied�" size="large" className="custom-button17" onClick={handleAddOdpowiedz} />
                                        </Menu.Item>
                                    )}
                                    {role === 'Teacher' && (
                                        <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedzi`}>
                                            <Button content="Odpowiedzi" size="large" className="custom-button17" onClick={handleOdpowiedz} />
                                        </Menu.Item>
                                    )}
                                </List.Content>
                            </List.Item>
                        ))}
                    </List>
                ) : (
                    <p>Brak zadań dla tego kursu.</p>
                )}
            </div>
        ) : (
            <div>
                <p>Nie masz dostępu do tego kursu.</p>
                <Link to="/kursy" className="btn btn-secondary">
                    Powrót do kursów
                </Link>
            </div>
        )
    );
}

export default KursDetails;
