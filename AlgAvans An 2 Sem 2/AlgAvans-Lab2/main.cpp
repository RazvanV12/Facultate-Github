#include <iostream>
#include <vector>

using namespace std;

class point{
    long long x, y;
public:
    point() = default;
    point(long long _x, long long _y){
        x = _x;
        y = _y;
    }
    point(point const &_point){
        x = _point.x;
        y = _point.y;
    }
    void set_x(long long _x){
        x = _x;
    }
    void set_y(long long _y){
        y = _y;
    }
    long long get_x()const {
        return x;
    }
    long long  get_y() const{
        return y;
    }
    point operator+(const point &point1) const{
        return point(x + point1.get_x() , y + point1.get_y());
    }
    point operator-(const point &point1) const{
        return point(x - point1.get_x(), y - point1.get_y());
    }
    long long orientation_test(const point &P, const point &Q)const{
        return Q.get_x() * y + P.get_x() * Q.get_y() + P.get_y() * x - Q.get_x() * P.get_y() - x * Q.get_y() - y*P.get_x();
    }
    bool is_on_segment(const point &point1, const point &point2)const{
        long long x_max = point1.get_x(), x_min = point2.get_x();
        if(x_max < x_min){
            x_max = point2.get_x();
            x_min = point1.get_x();
        }
        if(this->orientation_test(point1, point2) == 0 && this->x >= x_min && this->x <= x_max)
            return true;
        return false;
    }

};

bool intersection_in_middle(const point &seg1_point1, const point &seg1_point2, const point &seg2_point1, const point &seg2_point2){
    if((seg1_point1.orientation_test(seg2_point1,seg2_point2) > 0 && seg1_point2.orientation_test(seg2_point1, seg2_point2) < 0) ||
       (seg1_point1.orientation_test(seg2_point1,seg2_point2) < 0 && seg1_point2.orientation_test(seg2_point1, seg2_point2) > 0))
        if((seg2_point1.orientation_test(seg1_point1,seg1_point2) > 0 && seg2_point2.orientation_test(seg1_point1,seg1_point2) < 0) ||
           (seg2_point1.orientation_test(seg1_point1,seg1_point2) < 0 && seg2_point2.orientation_test(seg1_point1,seg1_point2) > 0))
            return true;
    return false;
}

void pozition_of_point_in_convex_polygon(){
    int n, m;
    long long x_aux, y_aux;
    point point_aux;
    vector<point>polygon ;
    vector<point>points;

    // citire date de la tastatura
    cin >> n;
    for (int i = 0; i < n; i++){
        cin >> x_aux >> y_aux;
        point_aux.set_x(x_aux);
        point_aux.set_y(y_aux);
        polygon.push_back(point_aux);
    }
    cin >> m;
    for(int i = 0; i < m; i++){
        cin >> x_aux >> y_aux;
        point_aux.set_x(x_aux);
        point_aux.set_y(y_aux);
        points.push_back(point_aux);
    }

    point origin(polygon[0]);
    for(int k = 0; k < m; k ++){
        int left = n - 1, right = 1, mid;
        long long aux;

        // determinam triunghiul in O(logn)
        while(left - right != 1){
            mid = (left + right) / 2;
            aux = points[k].orientation_test(origin, polygon[mid]);
            if (aux >= 0)
                right = mid;
            else
                left = mid;
        }

        aux = points[k].orientation_test(polygon[left], polygon[right]);
        if(aux == 0)
            cout << "BOUNDARY" << endl;
        else
            if(aux < 0)
                if (left == n - 1 && points[k].orientation_test(origin, polygon[left])== 0)
                    cout << "BOUNDARY" << endl;
                else
                    if(right == 1 && points[k].orientation_test(origin, polygon[right]) == 0)
                        cout << "BOUNDARY" << endl;
                    else
                        cout << "INSIDE" << endl;
            else
                cout << "OUTSIDE" << endl;
        }
}


void position_of_point_in_polygon(){
    int n, m;
    vector<point>polygon;
    vector<point>points;
    long long x_aux, y_aux;
    point point_aux{};

    // citire
    cin >> n;
    for (int i = 0; i < n; i++){
        cin >> x_aux >> y_aux;
        point_aux.set_x(x_aux);
        point_aux.set_y(y_aux);
        polygon.push_back(point_aux);
    }
    cin >> m;
    for(int i = 0; i < m; i++){
        cin >> x_aux >> y_aux;
        point_aux.set_x(x_aux);
        point_aux.set_y(y_aux);
        points.push_back(point_aux);
    }

    // luam punctul M random departe de poligon
    long long x_max = polygon[0].get_x(), y_max = polygon[0].get_y();
    for(int i = 1; i < n; i++){
        if(polygon[i].get_x() > x_max)
            x_max = polygon[i].get_x();
        if(polygon[i].get_y() > y_max)
            y_max = polygon[i].get_y();
    }
    point random_point(x_max + 1, y_max + 1);
    int i;

    for(int k = 0; k < m; k++){
        int counter = 0;
        for(i = 0; i < n - 1; i++){
            if(points[k].is_on_segment(polygon[i], polygon[i+1]))
                break;
        }
        if(i < n - 1) {
            cout << "BOUNDARY" << endl;
            continue;
        }

        if(points[k].is_on_segment(polygon[n - 1], polygon[0])){
            cout << "BOUNDARY" << endl;
            continue;
        }

        for(i = 0; i < n - 1; i++){
            if(polygon[i].orientation_test(points[k], random_point) == 0)
                random_point.set_x(random_point.get_x() + 1);
            if(polygon[i + 1].orientation_test(points[k], random_point) == 0)
                random_point.set_x(random_point.get_x() + 1);
            if(intersection_in_middle(points[k], random_point, polygon[i], polygon[i + 1]))
                counter++;
        }
        if(polygon[n - 1].orientation_test(points[k], random_point) == 0)
            random_point.set_x(random_point.get_x() + 1);
        if(polygon[0].orientation_test(points[k], random_point) == 0)
            random_point.set_x(random_point.get_x() + 1);
        if(intersection_in_middle(points[k], random_point, polygon[n - 1], polygon[0]))
            counter++;

        if(counter % 2 == 0)
            cout << "OUTSIDE" << endl;
        else
            cout << "INSIDE" << endl;
    }
}

void polygon_monotone(){
    int n, counter_x = 0, counter_y = 0;
    vector<point>polygon;

    long long x_aux, y_aux;
    point point_aux{};

    cin >> n;
    for (int i = 0; i < n; i++){
        cin >> x_aux >> y_aux;
        point_aux.set_x(x_aux);
        point_aux.set_y(y_aux);
        polygon.push_back(point_aux);
    }

    if(polygon[1].get_y() < polygon[0].get_y() && polygon[n - 1].get_y() < polygon[0].get_y())
        counter_y++;
    if(polygon[n - 2].get_y() < polygon[n - 1].get_y() && polygon[0].get_y() < polygon[n - 1].get_y())
        counter_y++;
    for(int i = 1; i < n - 1; i++){
        if(polygon[i - 1].get_y() < polygon[i].get_y() && polygon[i + 1].get_y() < polygon[i].get_y())
            counter_y++;
    }

    if(polygon[1].get_x() < polygon[0].get_x() && polygon[n - 1].get_x() < polygon[0].get_x())
        counter_x++;
    if(polygon[n - 2].get_x() < polygon[n - 1].get_x() && polygon[0].get_x() < polygon[n - 1].get_x())
        counter_x++;
    for(int i = 1; i < n - 1; i++){
        if(polygon[i - 1].get_x() < polygon[i].get_x() && polygon[i + 1].get_x() < polygon[i].get_x())
            counter_x++;
    }

    if(counter_x > 1)
        cout << "NO" << endl;
    else
        cout << "YES"<<endl;

    if(counter_y > 1)
        cout << "NO" << endl;
    else
        cout << "YES"<<endl;
}

int main() {
    return 0;
}
