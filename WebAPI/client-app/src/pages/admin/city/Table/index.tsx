import Paper from '@mui/material/Paper';

import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import CityCreate from '../Create';
import CityUpdate from '../Update';

import TableComponent from '../../../../components/Table';
import TableCellComponent from '../../../../components/TableCell/TableCellComponent';
import TableCellActionComponent from '../../../../components/TableCell/TableCellActionComponent';

const CityTable = () => {
    const [loading, setLoading] = useState<boolean>(false);

    const { GetCities, DeleteCity } = useActions();
    const { cities } = useTypedSelector((store) => store.city);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "City";
            await GetCities();
            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    };

    const onDelete = async (id: number) => {
        await DeleteCity(id);
        await GetCities();
    }

    return (
        <>
            <CityCreate />
            <Paper>
                <TableComponent
                    headLabel={["Id", "Name", "City", "Action"]}
                    rowsPerPageOptions={[5, 10, 25]}
                    itemsCount={cities.length}
                    bodyItems={cities
                        .map((row, index) => {
                            return [
                                <TableCellComponent
                                    key={row.id}
                                    label={row.id}
                                />,
                                <TableCellComponent
                                    key={row.name}
                                    label={row.name}
                                />,
                                <TableCellComponent
                                    key={row.countryName}
                                    label={row.countryName}
                                />,
                                <TableCellActionComponent
                                    key={index}
                                    path={null}
                                    edit={<CityUpdate id={row.id} />}
                                    onDelete={() => onDelete(row.id)}
                                />
                            ]
                        })
                    }
                />
            </Paper>
        </>
    );
}

export default CityTable;