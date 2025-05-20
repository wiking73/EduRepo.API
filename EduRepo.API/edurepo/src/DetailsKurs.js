import React, { useEffect, useState } from 'react';
import { useParams, Link, NavLink, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Styles/Details.css';
import { Menu, Button, List } from 'semantic-ui-react';

function KursDetails() {
    const role = localStorage.getItem("role");
    const name = localStorage.getItem("username");
    const token = localStorage.getItem("authToken");
    const { id } = useParams();
    const navigate = useNavigate();

    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [kurs, setKurs] = useState(null);
    const [zadania, setZadania] = useState([]);

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
            setError('Nie udało się pobrać zadań.');
        }
    };

    const handleDeleteZadanie = async (zadanieId) => {
        if (window.confirm("Czy na pewno chcesz usunąć to zadanie?")) {
            try {
                await axios.delete(`https://localhost:7157/api/Zadanie/${zadanieId}`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

                setZadania((prev) => prev.filter((z) => z.idZadania !== zadanieId));
            } catch (err) {
                console.error("Błąd przy usuwaniu zadania:", err);
                alert("Nie udało się usunąć zadania.");
            }
        }
    };

    useEffect(() => {
        fetchKurs();
        fetchZadania();
    }, [id]);

    if (isLoading) return <p>Ładowanie danych kursu...</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;
    if (!kurs) return <p>Nie znaleziono kursu</p>;

    const zadaniaDlaKursu = zadania.filter((z) => z.idKursu === parseInt(id, 10));

    return (
        (kurs.userName === name || role === "Student" || role === "Teacher") ? (
            <div className="kurs-details">
                <h4>{kurs.nazwa}</h4>
                <h3>Szczegółowe Informacje</h3>
                <p><strong>Opis:</strong> {kurs.opisKursu}</p>
                <p><strong>Klasa:</strong> {kurs.klasa}</p>
                <p><strong>Rok akademicki:</strong> {kurs.rokAkademicki}</p>
                <Link to="/kursy" className="btn btn-secondary">Powrót do kursów</Link>

                {role === "Teacher" && (
                    <div className="add-task-button">
                        <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/create`}>
                            <Button content="Dodaj Zadanie" size="large" className="custom-button17" />
                        </Menu.Item>
                        <Menu.Item as={NavLink} to={`/kurs/${id}/uczestnicy`}>
                            <Button content="Uczestnicy" size="large" className="custom-button17" />
                        </Menu.Item>
                        <Menu.Item as={NavLink} to={`/kurs/${id}/zgloszenia`}>
                            <Button content="Zgłoszenia" size="large" className="custom-button17" />
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

                                    {zadanie.plikPomocniczy && (
                                        <p>
                                            <strong>Plik pomocniczy:</strong>{' '}
                                            <a
                                                href={`https://localhost:7157${zadanie.plikPomocniczy}`}
                                                target="_blank"
                                                rel="noopener noreferrer"
                                            >
                                                Pobierz
                                            </a>
                                        </p>
                                    )}

                                    <Link to={`/zadanie/${zadanie.idZadania}`} className="btn btn-primary">
                                        Szczegóły zadania
                                    </Link>

                                    {role === "Student" && (
                                        <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedz`}>
                                            <Button content="Dodaj Odpowiedź" size="large" className="custom-button17" />
                                        </Menu.Item>
                                    )}

                                    {role === 'Teacher' && (
                                        <>
                                            <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedzi`}>
                                                <Button
                                                    content="Odpowiedzi"
                                                    size="large"
                                                    className="custom-button17"
                                                    style={{ marginTop: '1rem', marginLeft: '-2rem' }}
                                                />
                                            </Menu.Item>
                                            <Menu.Item>
                                                <Button
                                                    content="Usuń"
                                                    size="small"
                                                    color="red"
                                                    onClick={() => handleDeleteZadanie(zadanie.idZadania)}
                                                    style={{ marginTop: '0rem', marginLeft: '-0.5rem' }}
                                                />
                                            </Menu.Item>
                                            <Menu.Item as={NavLink} to={`/zadanie/edit/${zadanie.idZadania}`}>
                                                <Button
                                                    content="Edytuj"
                                                    size="small"
                                                    color="blue"
                                                    style={{ marginTop: '0.5rem', marginLeft: '0.5rem' }}
                                                />
                                            </Menu.Item>
                                        </>
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
                <Link to="/kursy" className="btn btn-secondary">Powrót do kursów</Link>
            </div>
        )
    );
}

export default KursDetails;
