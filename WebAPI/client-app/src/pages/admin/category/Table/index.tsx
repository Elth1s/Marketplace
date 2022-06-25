import Container from '@mui/material/Container';
import Paper from '@mui/material/Paper';
import Button from '@mui/material/Button';

import { useEffect, useState } from "react";

import { useActions } from "../../../../hooks/useActions";
import { useTypedSelector } from "../../../../hooks/useTypedSelector";

import TableComponent from '../../../../components/Table';
import TableCellComponent from '../../../../components/TableCell/TableCellComponent';
import TableCellActionComponent from '../../../../components/TableCell/TableCellActionComponent';
import TableCellImageComponent from '../../../../components/TableCell/TableCellImageComponent';

const CategoryTable = () => {
    const { GetCategory, DeleteCategory } = useActions();
    const [loading, setLoading] = useState<boolean>(false);

    const { categories } = useTypedSelector((store) => store.category);

    useEffect(() => {
        getData();
    }, []);

    const getData = async () => {
        setLoading(true);
        try {
            document.title = "Category";
            await GetCategory();
            setLoading(false);
        } catch (ex) {
            setLoading(false);
        }
    };

    const onDelete = async (id: number) => {
        await DeleteCategory(id);
        await GetCategory();
    }

    return (
        <>
            <Button
                variant="contained"
                href="/category/create"
                sx={{
                    my: 2,
                    px: 4,
                }}>
                Create
            </Button>
            <Paper>
                <TableComponent
                    headLabel={["Id", "Name", "Parent", "Action"]}
                    rowsPerPageOptions={[1, 5, 10, 25]}
                    itemsCount={categories.length}
                    bodyItems={categories
                        .map((row, index) => {
                            return [
                                <TableCellComponent
                                    key={row.id}
                                    label={row.id}
                                />,
                                <TableCellImageComponent
                                    key={row.name}
                                    image={row.image}
                                    label={row.name}
                                />,
                                <TableCellComponent
                                    key={row.parentName}
                                    label={row.parentName}
                                />,
                                <TableCellActionComponent
                                    key={index}
                                    path={"category/update?id=" + row.id}
                                    edit={null}
                                    onDelete={() => onDelete(row.id)}
                                />,
                            ]
                        })
                    }
                />
            </Paper>
        </>
    );
}

export default CategoryTable;