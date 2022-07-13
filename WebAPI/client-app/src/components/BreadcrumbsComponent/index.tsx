import {
    Typography,
    Breadcrumbs
} from '@mui/material';
import { NavigateNext } from '@mui/icons-material';
import { FC } from 'react';

import { ICatalogItem } from '../../pages/default/Catalog/types';

import LinkRouter from '../LinkRouter';

interface Props {
    parents: Array<ICatalogItem>,
}

const BreadcrumbsComponent: FC<Props> = ({ parents }) => {
    return (
        <Breadcrumbs aria-label="breadcrumb" sx={{ marginBottom: "30px" }} separator={<NavigateNext sx={{ color: "#7e7e7e" }} fontSize="small" />} >
            <LinkRouter underline="none" color="common.black" to="/">
                Home
            </LinkRouter>
            {parents.length == 0
                ? <Typography color="#7e7e7e">
                    Catalog
                </Typography>
                : <LinkRouter underline="none" color="common.black" to="/catalog">
                    Catalog
                </LinkRouter>}
            {parents.map((value, index) => {
                const last = index === parents.length - 1;

                return last ? (
                    <Typography color="#7e7e7e" key={index}>
                        {value.name}
                    </Typography>
                ) : (
                    <LinkRouter underline="none" color="common.black" to={`/catalog/${value.urlSlug}`} key={index}>
                        {value.name}
                    </LinkRouter>
                );
            })}
        </Breadcrumbs>
    );
};

export default BreadcrumbsComponent;