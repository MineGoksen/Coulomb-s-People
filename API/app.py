from flask import Flask, render_template, request, redirect, url_for
from flask_sqlalchemy import SQLAlchemy
from flask.templating import render_template
from flask_migrate import Migrate, migrate
from flask import Flask, jsonify


app = Flask(__name__)
app.debug = False
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///site.db'
db = SQLAlchemy(app)
migrate = Migrate(app, db)

class User(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    username = db.Column(db.String(80), unique=True, nullable=False)
    password = db.Column(db.String(120), unique=False, nullable=False)

    def __init__(self, username, password):
        self.username = username
        self.password = password

"""
admin = User("PentaVR", "PentaVR1234*")
db.session.add(admin)
db.session.commit()
"""

class Question(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    question = db.Column(db.String(1000), nullable=False)
    choice_a = db.Column(db.String(500), nullable=False)
    choice_b = db.Column(db.String(500), nullable=False)
    choice_c = db.Column(db.String(500), nullable=False)
    choice_d = db.Column(db.String(500), nullable=False)
    country = db.Column(db.String(500), nullable=False)
    right_answer = db.Column(db.String(500), nullable=False)

    def __init__(self, question, choice_a, choice_b, choice_c, choice_d, country, right_answer):
        self.question = question
        self.choice_a = choice_a
        self.choice_b = choice_b
        self.choice_c = choice_c
        self.choice_d = choice_d
        self.country = country
        self.right_answer = right_answer
    def to_api(self):
        return {'id': self.id, 'question': self.question, 'choice_a': self.choice_a, 'choice_b': self.choice_b, 'choice_c': self.choice_c, 'choice_d': self.choice_d, 'country': self.country, 'right_answer': self.right_answer}
    def to_api_with_country(self,country):
        if country==self.country: 
            return {'id': self.id, 'question': self.question, 'choice_a': self.choice_a, 'choice_b': self.choice_b, 'choice_c': self.choice_c, 'choice_d': self.choice_d, 'country': self.country, 'right_answer': self.right_answer}
@app.route('/questionsApi/<country>', methods=['GET'])
def get_question_country(country):
    questions = Question.query.all()
    questions_api = [question.to_api_with_country(country) for question in questions]
    return jsonify(questions_api)
@app.route('/questionsApi', methods=['GET'])
def get_questions():
    questions = Question.query.all()
    questions_api = [question.to_api() for question in questions]
    return jsonify(questions_api)

class Tip(db.Model):
    id = db.Column(db.Integer, primary_key=True, autoincrement=True)
    tip = db.Column(db.String(500), nullable=False )
    country = db.Column(db.String(500), nullable=False)
    def __init__(self, tip, country):
        self.tip = tip
        self.country = country
    def to_api2(self):
        return {'id': self.id, 'tip': self.tip, 'country': self.country}
    def to_api2_with_country(self,country):
        if country==self.country:
            return {'id': self.id, 'tip': self.tip, 'country': self.country}    

@app.route('/tipApi', methods=['GET'])
def get_tip():
    tip = Tip.query.all()
    tip_api = [tip.to_api2() for tip in tip]
    return jsonify(tip_api)
@app.route('/tipApi/<country>', methods=['GET'])
def get_tip_country(country):
    tip = Tip.query.all()
    tip_api = [tip.to_api2_with_country(country) for tip in tip]
    return jsonify(tip_api)


@app.route('/')
def index():
    return render_template('login.html')

@app.route('/login', methods=['POST'])
def login():
    username = request.form['username']
    password = request.form['password']
    user = User.query.filter_by(username=username, password=password).first()
    if user:
        return redirect(url_for('home'))
    else:
        return render_template('login.html',mesaj="Password or username is incorrect")

@app.route('/home')
def home():
    return render_template('add_question.html')

@app.route('/add_question', methods=['POST'])
def add_question():
    question = request.form['question']
    choice_a = request.form['choice_a']
    choice_b = request.form['choice_b']
    choice_c = request.form['choice_c']
    choice_d = request.form['choice_d']
    country = request.form['country']
    right_answer = request.form['right_answer']
    new_question = Question(question, choice_a, choice_b, choice_c, choice_d, country, right_answer)
    db.session.add(new_question)
    db.session.commit()
    return redirect(url_for('home'))


@app.route('/add_tip', methods=['POST'])
def add_tip():
    country = request.form['country']
    tip = request.form['tip']
    new_question = Tip(tip, country)
    db.session.add(new_question)
    db.session.commit()
    return redirect(url_for('home'))



if __name__ == '__main__':
    app.run()



