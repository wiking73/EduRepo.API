import React, { useEffect, useState } from 'react';
import { useParams, Link, NavLink, useNavigate } from 'react-router-dom';
import axios from 'axios';
import './Styles/Details.css';
import { Menu, Button, List } from 'semantic-ui-react';

// Interfejs dla kursu
interface Kurs {
    idKursu: number;
    nazwa: string;
    opisKursu: string;
    rokAkademicki: string;
    klasa: string;
    userName: string;
}

// Interfejs dla zadania
interface Zadanie {
    idZadania: number;
    nazwa: string;
    tresc: string;
    terminOddania: string;
    plikPomocniczy: string;
    czyObowiazkowe: boolean;
    wlascicielId: string;
    userName: string;
    idKursu: number; // Zak³adam, ¿e zadanie ma przypisane id kursu
}
const role = localStorage.getItem("role");
function KursDetails() {
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<null | string>(null);
    const [kurs, setKurs] = useState<Kurs | null>(null);
    const [zadania, setZadania] = useState<Zadanie[]>([]); // Tablica z zadaniami

    const { id } = useParams();
    const navigate = useNavigate();

    // Funkcja do pobierania szczegó³ów kursu
    const fetchKurs = async () => {
        try {
            const response = await axios.get(`https://localhost:7157/api/Kurs/${id}`);
            setKurs(response.data);
        } catch (err) {
            console.error('B³¹d podczas pobierania kursu:', err);
        } finally {
            setIsLoading(false);
        }
    };

    // Funkcja do pobierania zadañ dla kursu
    const fetchZadania = async () => {
        try {
            const response = await axios.get(`https://localhost:7157/api/Zadanie`);
            setZadania(response.data);
        } catch (err) {
            console.error('B³¹d podczas pobierania zadañ:', err);
        }
    };

    useEffect(() => {
        fetchKurs();
        fetchZadania(); // Pobieranie zadañ po za³adowaniu kursu
    }, [id]);

    if (isLoading) {
        return <p>£adowanie danych kursu...</p>;
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
        console.log('Dodaj zadanie dla kursu (tylko Admin)');
    };
    const handleOdpowiedz = () => {
        console.log('Zobacz Odpowiedzi');
    };
    
    const zadaniaDlaKursu = zadania.filter((zadanie) => zadanie.idKursu === parseInt(id!));
    const role = localStorage.getItem('role');

    return (
        <div className="kurs-details">
            <h4>{kurs.nazwa}</h4>
            <h3>Szczegó³owe Informacje</h3>
            <p><strong>Opis:</strong> {kurs.opisKursu}</p>
            <p><strong>Klasa:</strong> {kurs.klasa}</p>
            <p><strong>Rok akademicki:</strong> {kurs.rokAkademicki}</p>
            <Link to="/kursy" className="btn btn-secondary">
                Powrót do kursów
            </Link>

            {role === "Teacher" && (
            
            <div className=".add-task-button">
                <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/create`}>
                    <Button content="Dodaj Zadanie" size="large" className="custom-button17" onClick={handleAddZadanie} />
                </Menu.Item>
            </div>

    )
};
            <p><strong>Stworzony przez:</strong> {kurs.userName}</p>

            
            <h3>Lista zadañ</h3>
            {zadaniaDlaKursu.length > 0 ? (
                <List>
                    {zadaniaDlaKursu.map((zadanie) => (
                        <List.Item key={zadanie.idZadania}>
                            <List.Content>
                                <List.Header>{zadanie.nazwa}</List.Header>
                                <p><strong>Termin oddania:</strong> {new Date(zadanie.terminOddania).toLocaleString()}</p>
                                <p>{zadanie.tresc}</p>
                                <Link to={`/zadanie/${zadanie.idZadania}`} className="btn btn-primary">
                                    Szczegó³y zadania
                                </Link>
                                {role === "Student" && (
                                    <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedz`}>
                                        <Button content="Dodaj OdpowiedŸ" size="large" className="custom-button17" onClick={handleAddOdpowiedz} />
                                    </Menu.Item>
                                )};
                                {role === 'Teacher' && (

                                    <Menu.Item as={NavLink} to={`/kurs/${id}/zadanie/${zadanie.idZadania}/odpowiedzi`}>
                                        <Button content="Odpowiedzi" size="large" className="custom-button17" onClick={handleOdpowiedz} />
                                    </Menu.Item>
                                ) };
                            </List.Content>
                        </List.Item>
                    ))}
                </List>
            ) : (
                <p>Brak zadañ dla tego kursu.</p>
            )}

        </div>
    );
}

export default KursDetails;
