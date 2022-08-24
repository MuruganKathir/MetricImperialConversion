
export interface User {
    id?: string;
    firstName?: string;
    lastName?: string;
    userName?: string;
    email?: string;
    phone?: string;
    lockoutEnabled?: boolean;
    roleName?: string;
    claims?: string[];
    emailConfirmed?: boolean;
}
