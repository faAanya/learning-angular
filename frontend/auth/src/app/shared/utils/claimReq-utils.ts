export const claimReq = {
    adminOnly: (claims: any) => claims.role == "ADMIN",
    adminOrTeacher:(claims: any) => claims.role == "ADMIN" || claims.role == "TEACHER",
    hasLibraryId:(claims:any)=> 'LibraryId' in claims,
    under18:(claims:any)=>parseInt(claims.age) < 18
}