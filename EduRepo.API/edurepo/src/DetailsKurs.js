import React, { useEffect, useState } from 'react';
import { useParams, Link, NavLink, useNavigate, useLocation } from 'react-router-dom';
import axios from 'axios';
import './Styles/Details.css';
import { Menu, Button, List } from 'semantic-ui-react';

function KursDetails() {
    
    const role = localStorage.getItem("role");
    const name = localStorage.getItem("displayName");
    const token = localStorage.getItem("authToken");
    const { id } = useParams();
    const navigate = useNavigate();

    const location = useLocation();
    const moje = location.state;
    const path = moje ? "/mojekursy" : "/kursy";

    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);
    const [kurs, setKurs] = useState(null);
    const [mojeKursy, setMojeKursy] = useState([]);
    const [zadania, setZadania] = useState([]);
    const [answers, setAnswers] = useState(null);


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

    const fetchMojeKursy = async (name) => {

        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${name}/mojekursy`);     
            setMojeKursy(response.data.map((k) => k.kursId));        
        } catch (err) {
            console.error('Błąd pobierania listy kursów:', err);
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

    const fetchOdpowiedz = async () => {
        try {
            const response = await axios.get('https://localhost:7157/api/Odpowiedz');
            setAnswers(response.data)
        } catch (err) {
            console.error('Błąd pobierania odpowiedzi:', err);
        } finally {
            //setLoading(false);
        }
    };

    const handleDeleteOdpowiedz = async (idOdpowiedzi) => {
        try {
            if (!token) {
                setError('Musisz być zalogowany!');
                return;
            }

            if (window.confirm('Czy na pewno chcesz usunąć tę odpowiedź?')) {
                await axios.delete(`https://localhost:7157/api/Odpowiedz/${idOdpowiedzi}`, {
                    headers: { Authorization: `Bearer ${token}` },
                });
                assignAnswers();
                navigate(0);
            }
        } catch (err) {
            console.error('Błąd usuwania:', err);
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
        fetchOdpowiedz();
        fetchMojeKursy(name);
    }, [id]);

    

    if (isLoading) return <p>Ładowanie danych kursu...</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;
    if (!kurs) return <p>Nie znaleziono kursu</p>;

    let odpowiedzDlaZadania;

    const assignAnswers = () => {
        odpowiedzDlaZadania = answers ? Object.fromEntries(zadaniaDlaKursu.map((zadanie) =>
            [zadanie.idZadania, answers.find((ans) => ans.idZadania === zadanie.idZadania && ans.userName === name) || null])) : {};    
    }
    const zadaniaDlaKursu = zadania.filter((z) => z.idKursu === parseInt(id, 10));
    assignAnswers();

    return (
        (kurs.userName === name || role === "Student" ) ? (
            <div className="kurs-details">
                <h4>{kurs.nazwa}</h4>
                <h3>Szczegółowe Informacje</h3>
                <p><strong>Opis:</strong> {kurs.opisKursu}</p>
                <p><strong>Klasa:</strong> {kurs.klasa}</p>
                <p><strong>Rok akademicki:</strong> {kurs.rokAkademicki}</p>
                <Link to={path} className="btn btn-secondary">Powrót do kursów</Link>

                {role === "Teacher"  && (
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

                {((role === "Teacher") || (role === "Student" && mojeKursy.includes(kurs.idKursu))) ? <>
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
                                        {role === "Student" && (
                                            odpowiedzDlaZadania[zadanie.idZadania] ? <List.Item>
                                                <h3><strong>Twoja odpowiedź</strong></h3>
                                                <p><strong>Data oddania <span></span>{new Date(odpowiedzDlaZadania[zadanie.idZadania].dataOddania).toLocaleString()} </strong>
                                                    {new Date(zadanie.terminOddania).toLocaleString() < new Date(odpowiedzDlaZadania[zadanie.idZadania].dataOddania).toLocaleDateString() && <span className="late-info">Oddane po terminie</span>}
                                                </p>
                                                <p><strong>Plik </strong>
                                                    <a href={`https://localhost:7157${odpowiedzDlaZadania[zadanie.idZadania].nazwaPliku}`} target="_blank" rel="noopener noreferrer">
                                                        Otwórz
                                                    </a>
                                                </p>
                                                <p><strong>Komentarz nauczyciela</strong> {odpowiedzDlaZadania[zadanie.idZadania].komentarzNauczyciela}</p>
                                                <p><strong>Ocena </strong>{odpowiedzDlaZadania[zadanie.idZadania].ocena}</p>
                                                <Button
                                                    content="Usuń odpowiedź"
                                                    size="small"
                                                    onClick={() => handleDeleteOdpowiedz(odpowiedzDlaZadania[zadanie.idZadania].idOdpowiedzi)}
                                                    style={{ backgroundColor: "red" }}
                                                />
                                            </List.Item> :
                                                <Link to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedz`}>
                                                    <Button content="dodaj odpowiedź" size="large" className="custom-button17" />
                                                </Link>)}

                                        {role === 'Teacher' && (
                                            <>
                                                <Link to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedzi`}>
                                                    <Button
                                                        content="Odpowiedzi"
                                                        size="large"
                                                        className="custom-button17"
                                                        style={{ marginTop: '1rem' }}
                                                    />
                                                </Link>
                                                <div>
                                                    <Button
                                                        content="Usuń"
                                                        size="small"
                                                        color="red"
                                                        onClick={() => handleDeleteZadanie(zadanie.idZadania)}
                                                        style={{ marginTop: '1rem' }}
                                                    />
                                                </div>
                                                <Link to={`/zadanie/edit/${zadanie.idZadania}`}>
                                                    <Button
                                                        content="Edytuj"
                                                        size="small"
                                                        color="blue"
                                                        style={{ marginTop: '0.5rem' }}
                                                    />
                                                </Link>
                                            </>
                                        )}
                                    </List.Content>
                                </List.Item>
                            ))}
                        </List>
                    ) : (
                        <p>Brak zadań dla tego kursu.</p>
                    )}
                </> :
                    <p>Nie masz dostepu do listy zadań</p>}

               
            </div>
        ) : (
            <div>
                <p>Nie masz dostępu do tego kursu.</p>
                    <Link to={path} className="btn btn-secondary">Powrót do kursów</Link>                  
            </div>
        )
    );
}

export default KursDetails;
