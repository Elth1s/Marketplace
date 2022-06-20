import Paper from '@mui/material/Paper';

import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import CountryCreate from '../Create';
import CountryUpdate from '../Update';

import TableComponent from '../../../../components/Table';
import TableCellComponent from '../../../../components/TableCell/TableCellComponent';
import TableCellActionComponent from '../../../../components/TableCell/TableCellActionComponent';

const CountryTable = () => {
    const [loading, setLoading] = useState<boolean>(false);

    const { GetCountries, DeleteCountry } = useActions();
    const { countries } = useTypedSelector((store) => store.country);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Characteristic Group";
            await GetCountries();
            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    };

    const onDelete = async (id: number) => {
        await DeleteCountry(id);
        getData();
    }

    return (
        <>
            <CountryCreate />
            <Paper>
                <TableComponent
                    headLabel={["Id", "Name", "Action"]}
                    rowsPerPageOptions={[1, 5, 10, 25]}
                    itemsCount={countries.length}
                    bodyItems={countries
                        .map((row, index) => {
                            return [
                                <TableCellComponent
                                    key={row.id}
                                    label={row.id} />,
                                <TableCellComponent
                                    key={row.name}
                                    label={row.name} />,
                                <TableCellActionComponent
                                    key={index}
                                    path={null}
                                    edit={<CountryUpdate id={row.id} />}
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

export default CountryTable